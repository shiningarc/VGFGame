using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;


//�������ڿ�����Ϸ�н�ɫ���ƶ�����Ծ�Ͷ���
public class VGF_Player : Singleton<VGF_Player>
{
    public int speed;
    public int jumpSpeed;
    public Transform foot;
    Rigidbody rb;
    Animator animator;
    private bool _mute = false;
    
    //�Ƿ�������ת
    public bool Mute
    {
        get
        {
            return _mute;
        }
        set
        {
            _mute = value;
            if (_mute)
                rb.constraints = RigidbodyConstraints.FreezeAll;
            else
                rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    //��Ծ״̬�����С���Ծ1����Ծ2��
    public enum JumpState
    {
        Idle, Jump1, Jump2
    }

    //��ɫ�ƶ�����ǰ�������ң�
    public enum Direction
    {
        Forward, Backward, Left, Right
    }

    //ʹ��FSM��������Ծ״̬
    public FSM<JumpState> fsm = new FSM<JumpState>();
    public Direction direction;
    public JumpState state;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        JumpStateInit();
    }

    void Update()
    {
        Move();
        state = (JumpState)fsm.CurrentState;
        fsm.update();
        //Debug.Log(Physics.OverlapSphere(foot.position, 1f, 1<<LayerMask.NameToLayer("Ground")).Length);
    }
    private void FixedUpdate()
    {
        fsm.FixedUpdate();
    }

    //��ɫ�ƶ����Ի�ȡ
    public void Move()
    {
        //��ȡ���뷽��
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");
        
        //���㲢�ı��ƶ��ٶ�
        rb.velocity = new Vector3(InputX * speed, rb.velocity.y, InputY * speed);
        var localscale = transform.localScale;
        localscale.x = Mathf.Sign(InputX);
        transform.localScale = localscale;

        //���Ž�ɫ���ƶ�����
        if (Mathf.Abs(InputX) > 0.01f || Mathf.Abs(InputY) > 0.01f)
        {
            animator.SetFloat("InputX", InputX);
            animator.SetFloat("InputY", InputY);
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);


    }

    //��ʼ��FSM
    public void JumpStateInit()
    {
        //��ʼ��վ��
        fsm.State(JumpState.Idle).
           OnUpdate(() =>
           {
               if (Input.GetKeyDown(KeyCode.K))
               {
                   fsm.ChangeState(JumpState.Jump1);
               }

           });

        //��ʼ����Ծ1
        fsm.State(JumpState.Jump1)
            .OnEnter(() =>
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpSpeed, 0);
            })
            .OnUpdate(() =>
            {
                if (Input.GetKeyDown(KeyCode.K))
                    fsm.ChangeState(JumpState.Jump2);
                if (Physics.OverlapSphere(foot.position, 0.1f, 1 << LayerMask.NameToLayer("Ground")).Length > 0)
                    fsm.ChangeState(JumpState.Idle);
            });

        //��ʼ����Ծ2
        fsm.State(JumpState.Jump2)
            .OnEnter(() =>
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpSpeed, 0);
            })
            .OnFixedUpdate(() =>
            {
                if (Physics.OverlapSphere(foot.position, 0.1f, 1 << LayerMask.NameToLayer("Ground")).Length > 0)
                    fsm.ChangeState(JumpState.Idle);
            });
        fsm.ChangeState(JumpState.Idle);
    }
}
