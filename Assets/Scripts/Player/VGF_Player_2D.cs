using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//该类用于控制2D的角色移动和播放移动动画
public class VGF_Player_2D : Singleton<VGF_Player_2D>
{
    public int speed;
    Rigidbody2D rb2D;
    Animator animator;
    public bool isDashing;

    [Header("Dash参数")]
    public float dashTime;
    private float dashTimeLeft;
    private float lastDash;
    private float dashFrameCnt;
    public float dashCoolDown;
    public float dashSpeed;
    public int dashFrames;

    private float lastBandage;
    public float BandageCoolDown;
    [Header("CD的UI组件")]
    public Image CDImage;
    //初始化属性，获取Rigidbody2D和Animator组件
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DataCollection.AddActionToSkillDic(ReadyToDash, "Dash");
        DataCollection.AddActionToSkillDic(Bandage, "Bandage");
        lastBandage = Time.time;
        lastDash = Time.time;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        DataCollection.AddActionToSkillDic(ReadyToDash,"Dash",false);
        DataCollection.AddActionToSkillDic(Bandage, "Bandage",false);
    }
    private bool _mute = false;

    //是否允许旋转，是否可以运动
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
                DataCollection.ReleaseSkill("Dash");
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(Time.time >= lastBandage + BandageCoolDown)
                DataCollection.ReleaseSkill("Bandage");
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
        //设置动画参数并播放移动动画
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

        //根据水平输入轴的值决定玩家的朝向
        localscale.x = Mathf.Sign(InputX);
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
