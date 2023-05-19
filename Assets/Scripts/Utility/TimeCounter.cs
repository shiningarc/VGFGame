using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public float setTime; 
    private bool isEnded;
    public float timeCnt;
    public Action timeOutEvent;

    public void StartCnt()
    {
        timeCnt = 0;
        isEnded = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (!isEnded)
        {
            if (timeCnt < setTime)
                timeCnt = timeCnt + Time.deltaTime;
            else
            {
                timeOutEvent?.Invoke();
                isEnded = true;
            } 
        }
        
    }
    


}
