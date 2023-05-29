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
    public Sprite[] LightSprites;
    public GameObject HoriLightIns;

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
            //HoriLightIns.GetComponent<SpriteRenderer>().sprite = LightSprites[0];
            colliders[0].enabled = true;
            colliders[1].enabled = true;
            colliders[2].enabled = true;
            colliders[3].enabled = true;
        }
        else if(timer > 10f)
        {
            HoriLightIns.GetComponent<SpriteRenderer>().sprite = LightSprites[0];
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
            HoriLightIns.GetComponent<SpriteRenderer>().sprite = LightSprites[1];
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
