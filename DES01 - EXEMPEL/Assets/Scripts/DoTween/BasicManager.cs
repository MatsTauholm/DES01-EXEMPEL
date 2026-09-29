using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using DG.Tweening;
using System;

public class BasicManager : MonoBehaviour
{
    private void Update()
    {
        OnPress();
    }

    private void OnPress()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // Left mouse button clicked
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // Convert mouse position to world coordinates
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero); // Cast a ray from the mouse position

            if (hit.transform != null) 
            {
                string objectName = hit.transform.gameObject.name;

                switch (objectName)
                {
                    case "Strawberry":
                        hit.transform.DOMove(new Vector2(3, 3), 1f);
                        break;
                    case "Pineapple":
                        hit.transform.DOScale(Vector2.zero, 1f);
                        break;
                    case "Melon":
                        hit.transform.DORotate(new Vector3(0, 0, 180), 1f);
                        break;
                }
            }
        }
    }
}
