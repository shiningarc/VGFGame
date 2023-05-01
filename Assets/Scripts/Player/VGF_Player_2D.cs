using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


//该类用于控制2D的角色移动和播放移动动画
public class VGF_Player_2D : Singleton<VGF_Player_2D>
{
    public int speed;
    Rigidbody2D rb2D;
    Animator animator;

    //初始化属性，获取Rigidbody2D和Animator组件
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            if (_mute)
                rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            else
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void Update()
    {
        //获取水平和垂直输入轴的值
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");

        //设置动画参数并播放移动动画
        if (Mathf.Abs(InputX) > 0.01f || Mathf.Abs(InputY) > 0.01f)
        {
            animator.SetFloat("InputX", InputX);
            animator.SetFloat("InputY", InputY);
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);
        rb2D.velocity = new Vector2(InputX, InputY) * speed;
        var localscale = transform.localScale;

        //根据水平输入轴的值决定玩家的朝向
        localscale.x = Mathf.Sign(InputX);
        transform.localScale = localscale;
    }
}
