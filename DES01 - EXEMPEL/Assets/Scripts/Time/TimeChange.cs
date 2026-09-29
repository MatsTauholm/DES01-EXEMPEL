using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeChange : MonoBehaviour
{
    float defaultFixedDeltaTime;

    void Start()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        SlowDownTime();
    }

    private void SlowDownTime()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            Time.timeScale = 0.2f; //Slowdown time
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale; //Make sure that fixedDeltaTime also scales since this does not happen automaticly
        }
        else
        {
            Time.timeScale = 1f; //Reset to default 
            Time.fixedDeltaTime = defaultFixedDeltaTime; //Reset fixedDeltaTime to default
        }
    }
}
