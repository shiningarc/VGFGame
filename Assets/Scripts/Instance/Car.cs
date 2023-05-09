using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform LeftBorder;
    public Transform RightBorder;
    public CarDirection carDirection;
    public List<Sprite> sprites;
    public int speed;
    private float timer;
    private float setTimer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    public enum CarDirection
    {
        Left, Right, Up, Down
    }

    private enum CarStates
    {
        Running,Waiting
    }
    FSM<CarStates> fsm = new FSM<CarStates>();
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        FSMInit();
        fsm.ChangeState(CarStates.Running);
    }
    public void FSMInit()
    {
        fsm.State(CarStates.Running).OnEnter(() =>
        {
            sr.sprite = sprites[Random.Range(0, sprites.Count)];
            gameObject.SetActive(true);
            speed = Random.Range(12, 20);
            //int rand = Random.Range(0, 2);
            if (carDirection == CarDirection.Right)
            {
                transform.position = LeftBorder.position;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                rb.velocity = new Vector2(speed, 0);
            }
            else if (carDirection == CarDirection.Left)
            {
                transform.position = RightBorder.position;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                rb.velocity = new Vector2(-speed, 0);
            }
        })
        .OnFixedUpdate(() =>
        {
            if (transform.position.x < LeftBorder.position.x || transform.position.x > RightBorder.position.x)
            {
                fsm.ChangeState(CarStates.Waiting);
            }
        })
        .OnExit(() =>
        {
            //gameObject.SetActive(false);
        });

        fsm.State(CarStates.Waiting).OnEnter(() =>
        {
            timer = 0;
            setTimer = Random.Range(0, 10);
        })
        .OnUpdate(() =>
        {
            timer = timer + Time.deltaTime;
            if (timer > setTimer)
            {
                timer = 0;
                fsm.ChangeState(CarStates.Running);
            }
        })
        .OnExit(() =>
        {
            //gameObject.SetActive(true);
        });

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            var rb_player= collision.collider.GetComponent<Rigidbody2D>();
            var vgf_player = collision.collider.GetComponent<VGF_Player_2D>();
            vgf_player.enabled = false;
            rb_player.velocity = new Vector2(rb.velocity.x * 4, 0f);
            rb.velocity = Vector2.zero;
           
            StartCoroutine(SpeedDownX(rb_player));  
            
            collision.collider.enabled = false;
            



            EventHandler.CallDoDamage2Player(1000);
        }
    }

    IEnumerator SpeedDownX(Rigidbody2D rb)
    {
        while(Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.5f),rb.velocity.y); 
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        fsm.update(); 
    }
    private void FixedUpdate()
    {
        fsm.FixedUpdate();
    }
}
