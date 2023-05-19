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
   
    void Start()
    {
        timer = 0;
    }

   
    void Update()
    {
        if(timer > SetTimer)
        {
            timer = 0;
            for (int i = 0; i < colliders.Count; i++)
            { 
                if (colliders[i].enabled)
                    colliders[i].enabled = false;
                else colliders[i].enabled = true;
            }
        }
        timer = timer + Time.deltaTime;
    }
}
