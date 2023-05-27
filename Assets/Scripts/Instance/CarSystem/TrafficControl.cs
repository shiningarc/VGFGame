using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.WSA;

public class TrafficControl : MonoBehaviour
{
    public List<Collider2D> colliders; 
    public float SetTimer;
    float timer;
    public bool flag;
   
    void Start()
    {
        timer = 0;
    }


    void Update()
    {
        if (timer > SetTimer)
        {
            timer = 0;
        }
        else if (timer > 16f)
        {
            colliders[0].enabled = true;
            colliders[1].enabled = true;
            colliders[2].enabled = true;
            colliders[3].enabled = true;
        }
        else if(timer > 10f)
        {
            colliders[0].enabled = true;
            colliders[1].enabled = true;
            colliders[2].enabled = false;
            colliders[3].enabled = false;
        }
        else if(timer > 6f)
        {
            colliders[0].enabled = true;
            colliders[1].enabled = true;
            colliders[2].enabled = true;
            colliders[3].enabled = true;
        }
        else if(timer > 0f)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = true;
            colliders[3].enabled = true;
        }
        else
        {
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = false;
            colliders[3].enabled = false;
        }
        timer = timer + Time.deltaTime;
    }
}
