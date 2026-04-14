using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class QTE : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] RectTransform[] safeZones;
    [SerializeField] float smoothTime = 3f;
    [SerializeField] float maxSpeed = 10f;

    private bool isSafe;
    private RectTransform pointerTransform;
    private Vector3 targetPosition;
    private Vector2 velocity = Vector2.zero;

    
    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
    }

    void OnInteract(InputValue button)
    {
        if(button.isPressed)
        {
            CheckSuccess();
        }
    }

    private void CheckSuccess()
    {
        foreach (RectTransform safeZone in safeZones) //Loop through all the safe zones
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null)) //Check if the pointers position is within any of the safe zones
            {
                isSafe = true;
                break; //If it is, set isSafe to true and break out of the loop
            }
        }

        if(isSafe)
        {
            Debug.Log("Safe!");
        }
        else
        {
            Debug.Log("Fail!");
        }
        isSafe = false;
    }
    

    void Update()
    {
        Move();
        ChangeDirection();
    }

    private void Move() //Move the pointer towards the target position using SmoothDamp for smooth movement
    {
        pointerTransform.position = Vector2.SmoothDamp(pointerTransform.position, targetPosition,ref velocity, smoothTime, maxSpeed);
    }

    private void ChangeDirection() //Change the direction of the pointer when it gets close to either point A or B
    {
        if (Vector2.Distance(pointerTransform.position, pointA.position) < 3f)
        {
            targetPosition = pointB.position;
        }
        else if (Vector2.Distance(pointerTransform.position, pointB.position) < 3f)
        {
            targetPosition = pointA.position;
        }
    }
}
