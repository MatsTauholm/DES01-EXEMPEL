using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deduktion : MonoBehaviour
{
    // Assign correct buttons in Inspector (by name or ID)
    public List<string> correctButtons = new List<string> { "A", "C" };

    private HashSet<string> pressedButtons = new HashSet<string>();

    public void OnButtonPressed(string buttonID)
    {
        pressedButtons.Add(buttonID);
        CheckSolution();
    }

    private void CheckSolution()
    {
        // Only check when same number of buttons pressed
        if (pressedButtons.Count != correctButtons.Count)
            return;

        // Check if all correct buttons are pressed
        foreach (string id in correctButtons)
        {
            if (!pressedButtons.Contains(id))
            {
                FailPuzzle();
                return;
            }
        }

        Success();
    }

    private void Success()
    {
        Debug.Log("Correct combination!");
    }

    private void FailPuzzle()
    {
        Debug.Log("Wrong combination!");
        pressedButtons.Clear(); // reset puzzle
    }
}

