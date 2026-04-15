using UnityEngine;

public class BreakObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenVersionPrefab; // Prefab of the broken version of the object

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Example trigger, replace with your actual condition
        {
            Break();
        }
    }

    void Break()
    {
        // Instantiate the broken version at the same position and rotation
        Instantiate(brokenVersionPrefab, transform.position, transform.rotation);
        // Destroy the original object
        Destroy(gameObject);
    }
}
