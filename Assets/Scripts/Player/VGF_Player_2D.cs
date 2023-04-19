using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VGF_Player_2D : Singleton<VGF_Player_2D>
{
    public int speed;
    Rigidbody2D rb2D;
    Animator animator;
    void Start()
    { 
        rb2D = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
    }
    private bool _mute = false;

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
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");
        if (Mathf.Abs(InputX) > 0.01f || Mathf.Abs(InputY) > 0.01f)
        {
            animator.SetFloat("InputX", InputX);
            animator.SetFloat("InputY", InputY);
            animator.SetBool("Move", true);
        } 
        else animator.SetBool("Move", false);
        rb2D.velocity = new Vector2(InputX, InputY) * speed;
        var localscale = transform.localScale;
        localscale.x = Mathf.Sign(InputX);
        transform.localScale = localscale;
       
    }
}

