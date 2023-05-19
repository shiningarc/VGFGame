using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndIteract : MonoBehaviour,ICharacter,IInputProvider
{
    public event Action OnJump;
    public enum IsIteract
    {
        Yes,No
    }
    public IsIteract isIteract;
    public InputState GetState()
    {
        if(isIteract == IsIteract.Yes)
            return new InputState() { movement = new Vector2(0, 0),cover = true };
        else
            return new InputState() { cover = false };
    }

    public void OnInteract()
    {
        isIteract = IsIteract.Yes;
    }

    public void OnPlayerEnter()
    {
       
    }

    public void OnPlayerExit()
    {
        isIteract = IsIteract.No;
    }

    
    void Start()
    {
        isIteract = IsIteract.No;
    }

    
    void Update()
    {
        
    }
}
