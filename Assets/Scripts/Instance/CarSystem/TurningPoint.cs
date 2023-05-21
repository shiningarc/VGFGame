using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoint : MonoBehaviour
{
    public List<Car.CarDirection> Directions = new List<Car.CarDirection>();
    public Car.CarDirection rightDirection;
    public List<Car.CarDirection> GetDirections()
    {
        return Directions;
    }
}
