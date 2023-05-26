using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//�������ڿ���2D�Ľ�ɫ�ƶ��Ͳ����ƶ�����
public class VGF_Player_2D : Singleton<VGF_Player_2D>
{
    public int speed;
    Rigidbody2D rb2D;
    Animator animator;
    public bool isDashing;

    [Header("Dash����")]
    public float dashTime;
    private float dashTimeLeft;
    private float lastDash;
    private float dashFrameCnt;
    public float dashCoolDown;
    public float dashSpeed;
    public int dashFrames;

    private float lastBandage;
    public float BandageCoolDown;
    [Header("CD��UI���")]
    public Image CDImage;
    //��ʼ�����ԣ���ȡRigidbody2D��Animator���
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SkillSystem.AddActionToSkillDic(ReadyToDash, "Dash");
        SkillSystem.AddActionToSkillDic(Bandage, "Heal");
        lastBandage = Time.time;
        lastDash = Time.time;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        SkillSystem.AddActionToSkillDic(ReadyToDash,"Dash",false);
        SkillSystem.AddActionToSkillDic(Bandage, "Heal",false);
    }
    private bool _mute = false;

    //�Ƿ�������ת���Ƿ�����˶�
    public bool Mute
    {
        get
        {
            return _mute;
        }
        set
        {
            _mute = value;
            if (_mute && rb2D != null)
            {
                rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
                animator.SetBool("Move", false);
            }   
            else if(rb2D != null)
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private float InputX;
    private float InputY;
    private void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(Time.time >= lastDash+dashCoolDown && rb2D.velocity.magnitude > 1f)
            {
                SkillSystem.ReleaseSkill("Dash");
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(Time.time >= lastBandage + BandageCoolDown)
                SkillSystem.ReleaseSkill("Heal");
        }
        
    }
    private void FixedUpdate()
    {
        if (_mute) return;
        Dash();
        SwithAnim();
        if (isDashing)
            return;
        Move();
    }
    public void SwithAnim()
    {
        if (isDashing) return;
        //���ö��������������ƶ�����
        if (Mathf.Abs(InputX) > 0.01f || Mathf.Abs(InputY) > 0.01f)
        {
            animator.SetFloat("InputX", InputX);
            animator.SetFloat("InputY", InputY);
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);
    }
    public void Move()
    {
        rb2D.velocity = new Vector2(InputX, InputY) * speed;
        var localscale = transform.localScale;

        //����ˮƽ�������ֵ������ҵĳ���
        localscale.x = Mathf.Sign(animator.GetFloat("InputX"));
        transform.localScale = localscale;
    }
    void Bandage()
    {
        lastBandage = Time.time;
        EventHandler.CallDoDamage2Player(-10);
    }
    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }
    void Dash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                rb2D.velocity = rb2D.velocity.normalized * dashSpeed;
                dashTimeLeft = dashTimeLeft - Time.deltaTime;
                dashFrameCnt++;
                if(dashFrameCnt == dashFrames)
                {
                    ShadowPool.Instance.GetFromPool();
                    dashFrameCnt = 0;
                }
     
            }
            else if(dashTimeLeft <= 0)
            {
                isDashing = false;
                dashFrameCnt = 0;
            }
        }
    }
}
