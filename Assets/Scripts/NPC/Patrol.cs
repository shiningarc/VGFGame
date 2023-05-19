using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour,IInputProvider
{

    public List<Transform> targetPoints;
    public Transform currentTarget;
    public int targetIndex = 0;
    public int patrolSpeed;
    public bool isRandom;
    public float InputX;
    public float InputY;

    public event System.Action OnJump;

    void Start()
    {
        currentTarget = targetPoints[0];
    }

    

    public InputState GetState()
    {
        float distance = Vector2.Distance(currentTarget.position, transform.position);
        if(distance < 0.1f)
        {
            if(isRandom)
            {
                currentTarget = targetPoints[Random.Range(0, targetPoints.Count)];
            }
            else
            {
                targetIndex++;
                if (targetIndex == targetPoints.Count)
                    targetIndex = 0;
                currentTarget = targetPoints[targetIndex];
            }
            return new InputState() { cover = false };
        }
        else
        {
            Vector3 direction = Vector3.Normalize(currentTarget.position - transform.position);
            InputX = direction.x * patrolSpeed;
            InputY = direction.y * patrolSpeed;
            return new InputState() { movement = new Vector2(InputX, InputY), cover = true};
        }
    }


}
