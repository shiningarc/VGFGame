using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    public List<Car> cars = new List<Car>();
    public List<Sprite> spriteLists;
    private int index;
    private float timer;
    private float setTimer;
    public int minTime;
    public int maxTime;
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
            setTimer = Random.Range(minTime, maxTime);
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
    public void CarInit(Car car)
    {
        car.sprites.Clear();
        int carType = Random.Range(0, spriteLists.Count/4);
        for(int i = carType*4; i < carType * 4 + 4; i++)
        {
            car.sprites.Add(spriteLists[i]);
        }
    }
}
