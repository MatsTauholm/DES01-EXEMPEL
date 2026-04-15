using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    [SerializeField] private float minDistance = 0.1f; // Minimum distance to add a new point
    [SerializeField, Range(0,2)] private float lineWidth = 0.1f; // Width of the line

    private LineRenderer lineRenderer;
    private Vector3 previousPosition;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1; // Start with one point
        previousPosition = transform.position;
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
    }

    void Update()
    {
        if(Input.GetMouseButton(0)) // Check if left mouse button is held down
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0; // Set z to 0 for 2D drawing
            if (Vector3.Distance(previousPosition, currentPosition) > minDistance) // Add a new point if the mouse has moved enough
            {
                if (previousPosition == transform.position) // If it's the first point, set it to the current position
                {
                    lineRenderer.SetPosition(0, currentPosition);
                }
                else // Add a new point to the line renderer
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
                }
                previousPosition = currentPosition;
            }
        }
    }
}
