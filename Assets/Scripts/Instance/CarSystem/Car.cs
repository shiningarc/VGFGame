using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Car : MonoBehaviour
{ 
    public Transform Warning_Sign_LR;
    public Transform Warning_Sign_U;
    public Transform Warning_Sign_B;
    public CarDirection carDirection;
    public CarDirection InitDirection;
    public Transform InitPoint;
    public List<Sprite> sprites;
    public int speed;
    private float timer;
    private float setTimer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform CurrentWarning;
    private Transform CurrentTarget;
    private int CurrentTargetIndex;
    private BoxCollider2D coll;
    public bool warning;
    
    public enum CarDirection
    {
        Left, Right, Up, Down
    }

    public enum CarStates
    {
        Starting,Running,Stoping,Restarting,Ending,Waiting,Turning
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
        coll = GetComponent<BoxCollider2D>();  
        FSMInit();
        //FSMInit();
        fsm.ChangeState(CarStates.Starting); 
        
    }
    public void FSMInit()
    {

        fsm.State(CarStates.Starting).OnEnter(() =>
        {
            //sr.sprite = sprites[Random.Range(0, sprites.Count)];
            speed = Random.Range(10, 16);
            //int rand = Random.Range(0, 2);
            carDirection = InitDirection;
            transform.position = InitPoint.transform.position;
            ChangeCarDirection();

            fsm.ChangeState(CarStates.Running);
        });
        fsm.State(CarStates.Turning).OnEnter(() =>
        {
            ChangeCarDirection();
            fsm.ChangeState(CarStates.Running);
        });
        
        fsm.State(CarStates.Running)
        .OnFixedUpdate(() =>
        {
            if (warning)
            {
                fsm.ChangeState(CarStates.Stoping);
            }

        });
       

        fsm.State(CarStates.Stoping).OnEnter(() =>
        {
            if (carDirection == CarDirection.Left || carDirection == CarDirection.Right)
                StartCoroutine(SpeedDownX(rb));
            else
                StartCoroutine(SpeedDownY(rb));
        })
        .OnFixedUpdate(() =>
        {
                if(!warning) fsm.ChangeState(CarStates.Waiting);
        });

        fsm.State(CarStates.Waiting).OnEnter(() =>
        {
            setTimer = Random.Range(7, 15) / 15.0f;
            timer = 0;
        }).OnFixedUpdate(() =>
        {
            timer = timer + Time.deltaTime;
            if (warning)
            {
                fsm.ChangeState(CarStates.Stoping);

            }                
            else if (timer > setTimer)
                fsm.ChangeState(CarStates.Restarting);

        });

        fsm.State(CarStates.Restarting).OnEnter(() =>
        {
            if(carDirection == CarDirection.Left|| carDirection == CarDirection.Right)
            {
                int direction = carDirection == CarDirection.Right ? 1 : -1;
                StartCoroutine(SpeedUpX(rb, speed * direction));
            }
            else
            {
                int direction = carDirection == CarDirection.Up ? 1 : -1;
                StartCoroutine(SpeedUpY(rb, speed * direction));
            }
            
           
        })
        .OnFixedUpdate(() =>
        {
            if (warning)
            {
                fsm.ChangeState(CarStates.Stoping);
            }
            else if (rb.velocity.magnitude > speed - 0.5f)
            {
                fsm.ChangeState(CarStates.Running);
            }
        });
        fsm.State(CarStates.Ending).OnEnter(() =>
        {
            gameObject.SetActive(false);
        });


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("TurningPoint"))
        {
            TurningPoint turningPoint = other.GetComponent<TurningPoint>();
            if (turningPoint.rightDirection == carDirection)
            {
                var carDirectionList = other.GetComponent<TurningPoint>().GetDirections();
                carDirection = carDirectionList[Random.Range(0, carDirectionList.Count)];
                fsm.ChangeState(CarStates.Turning);
                //transform.position = other.transform.position;
            }
        }
        else if(other.CompareTag("Edge"))
        {
            fsm.ChangeState(CarStates.Ending);
        }
    }


    public void ChangeCarDirection()
    {
        if (carDirection == CarDirection.Right)
        {
            sr.sprite = sprites[0];
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            rb.velocity = new Vector2(speed, 0);
            coll.size = new Vector2(4.8f, 1.7f);
            CurrentWarning = Warning_Sign_LR;
        }
        else if (carDirection == CarDirection.Left)
        {
            sr.sprite = sprites[1];
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            rb.velocity = new Vector2(-speed, 0);
            coll.size = new Vector2(4.8f, 1.7f);
            CurrentWarning = Warning_Sign_LR;
        }           //gameObject.SetActive(true);
        else if (carDirection == CarDirection.Up)
        {
            sr.sprite = sprites[2];
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            rb.velocity = new Vector2(0, speed);
            coll.size = new Vector2(2f, 4.5f);
            CurrentWarning = Warning_Sign_U;
        }
        else if (carDirection == CarDirection.Down)
        {
            sr.sprite = sprites[3];
            //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            rb.velocity = new Vector2(0, -speed);
            coll.size = new Vector2(2f, 4.5f);
            CurrentWarning = Warning_Sign_B;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") && rb.velocity.magnitude > 5f)
        {
            var rb_player= collision.collider.GetComponent<Rigidbody2D>();
            var vgf_player = collision.collider.GetComponent<VGF_Player_2D>();
            vgf_player.enabled = false;
            rb_player.velocity = rb.velocity * 4f;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
           
            StartCoroutine(SpeedDownPlayer(rb_player));  
            
            collision.collider.enabled = false;

            EventHandler.CallDoDamage2Player(1000);
        }
    }
    #region ¼Ó¼õËÙ
    IEnumerator SpeedDownX(Rigidbody2D rb)
    {
        while(fsm.CurrentState == CarStates.Stoping)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.6f),rb.velocity.y); 
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    IEnumerator SpeedUpX(Rigidbody2D rb, float targetV)
    {
        while (fsm.CurrentState == CarStates.Restarting)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetV, 0.4f), rb.velocity.y);
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    IEnumerator SpeedUpY(Rigidbody2D rb, float targetV)
    {
        while (fsm.CurrentState == CarStates.Restarting)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, targetV, 0.4f));
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    IEnumerator SpeedDownY(Rigidbody2D rb)
    {
        while (fsm.CurrentState == CarStates.Stoping)
        {
            rb.velocity = new Vector2(rb.velocity.x , Mathf.Lerp(rb.velocity.y, 0, 0.6f));
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    
    IEnumerator SpeedDownPlayer(Rigidbody2D rb)
    {
        while (rb.velocity.magnitude > 0)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.5f), Mathf.Lerp(rb.velocity.y, 0, 0.5f));
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        fsm.update(); 
    }
    private void FixedUpdate()
    {
        fsm.FixedUpdate();
        if(carDirection == CarDirection.Left || carDirection == CarDirection.Right)
        {
            warning = Physics2D.OverlapBoxAll(CurrentWarning.position, new Vector2(4.8f,1.7f),0)
                      .Where((i) => { return i.CompareTag("Car");  }).Count() > 0;
        }
        else
        {
            warning = Physics2D.OverlapBoxAll(CurrentWarning.position, new Vector2(2f, 4.5f), 0)
                      .Where((i) => { return i.CompareTag("Car");  }).Count() > 0;
        }
        

    }
}
