using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    public List<Car> cars = new List<Car>();
    private int index;
    private float timer;
    private float setTimer;
    public Transform LeftBorder;
    public Transform RightBorder;
    void Start()
    {
        timer = 0;
        //cars[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= setTimer)
        {
            setTimer = Random.Range(1, 2);
            timer = 0;
            for(int i = 0; i < cars.Count; i++)
            {
                if (!cars[i].gameObject.activeInHierarchy)
                {
                    CarInit(cars[i]);
                    cars[i].gameObject.SetActive(true);
                    break;
                }
            }
        }
        timer = timer + Time.deltaTime;
    }
    void CarInit(Car car)
    {
        car.LeftBorder = LeftBorder;
        car.RightBorder = RightBorder;
    }
}
