using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform LeftBorder;
    public Transform RightBorder;
    public Transform Warning_Sign;
    public CarDirection carDirection;
    public List<Sprite> sprites;
    public int speed;
    private float timer;
    private float setTimer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public bool warning;
    
    public enum CarDirection
    {
        Left, Right, Up, Down
    }

    public enum CarStates
    {
        Starting,Running,Stoping,Restarting,Ending,Waiting
    }
    public FSM<CarStates> fsm = new FSM<CarStates>();
    /*private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 300, 300), fsm.CurrentState.ToString(),new GUIStyle { fontSize = 50});
    }
    /*private void Start()
    //{
        //rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        FSMInit();
        fsm.ChangeState(CarStates.Starting);
    }*/
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        FSMInit();
        //FSMInit();
        fsm.ChangeState(CarStates.Starting); 
    }
    public void FSMInit()
    {

        fsm.State(CarStates.Starting).OnEnter(() =>
        {
            sr.sprite = sprites[Random.Range(0, sprites.Count)];
            gameObject.SetActive(true);
            speed = Random.Range(10, 16);
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
            }           //gameObject.SetActive(true);
            fsm.ChangeState(CarStates.Running);
        });
        fsm.State(CarStates.Running)
        .OnFixedUpdate(() =>
        {
            if (transform.position.x < LeftBorder.position.x || transform.position.x > RightBorder.position.x)
            {
                fsm.ChangeState(CarStates.Ending);
            }
            else if (warning)
            {
                fsm.ChangeState(CarStates.Stoping);
            }

        });
       

        fsm.State(CarStates.Stoping).OnEnter(() =>
        {
            StartCoroutine(SpeedDownX(rb));
        })
        .OnFixedUpdate(() =>
        {
                if(!warning) fsm.ChangeState(CarStates.Waiting);
        });

        fsm.State(CarStates.Waiting).OnEnter(() =>
        {
            setTimer = Random.Range(0, 10) / 10.0f;
            timer = 0;
        }).OnFixedUpdate(() =>
        {
            timer = timer + Time.deltaTime;
            if (warning)
                fsm.ChangeState(CarStates.Stoping);
            else if (timer > setTimer)
                fsm.ChangeState(CarStates.Restarting);

        });

        fsm.State(CarStates.Restarting).OnEnter(() =>
        {

            int direction = carDirection == CarDirection.Right ? 1 : -1;
            StartCoroutine(SpeedUpX(rb, speed * direction));
        })
        .OnFixedUpdate(() =>
        {
            if (warning)
            {
                fsm.ChangeState(CarStates.Stoping);
            }
            else if (Mathf.Abs(rb.velocity.x) > speed - 0.5f)
            {
                fsm.ChangeState(CarStates.Running);
            }
            else if (transform.position.x < LeftBorder.position.x || transform.position.x > RightBorder.position.x)
            {
                fsm.ChangeState(CarStates.Ending);
            }
        });
        fsm.State(CarStates.Ending).OnEnter(() =>
        {
            gameObject.SetActive(false);
        });


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") && Mathf.Abs(rb.velocity.x) > 3f)
        {
            var rb_player= collision.collider.GetComponent<Rigidbody2D>();
            var vgf_player = collision.collider.GetComponent<VGF_Player_2D>();
            vgf_player.enabled = false;
            rb_player.velocity = new Vector2(rb.velocity.x * 4, 0f);
            rb.velocity = Vector2.zero;
           
            StartCoroutine(SpeedDownPlayerX(rb_player));  
            
            collision.collider.enabled = false;

            EventHandler.CallDoDamage2Player(1000);
        }
    }

    IEnumerator SpeedDownX(Rigidbody2D rb)
    {
        while(fsm.CurrentState == CarStates.Stoping)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.5f),rb.velocity.y); 
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    IEnumerator SpeedDownPlayerX(Rigidbody2D rb)
    {
        while (Mathf.Abs(rb.velocity.x) > 0)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.5f), rb.velocity.y);
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    IEnumerator SpeedUpX(Rigidbody2D rb,float targetV)
    {
        while (fsm.CurrentState == CarStates.Restarting)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetV, 0.5f), rb.velocity.y);
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

        warning = Physics2D.OverlapBoxAll(Warning_Sign.position, new Vector2(3.7f, 1.2f),0f)
                      .Where((i) => { return i.CompareTag("Car") || i.CompareTag("Player"); }).Count() > 0;

    }
}
