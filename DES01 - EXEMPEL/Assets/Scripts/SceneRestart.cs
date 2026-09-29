using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneRestart : MonoBehaviour
{

    void Update()
    {
        Restart();
    }

    private void Restart()
    {
        if(Keyboard.current.rKey.isPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }  
    }
}
