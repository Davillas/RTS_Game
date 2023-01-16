using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    private bool isSelected = false;

    public float speed = 5f; // Speed of movement
    public float acceleration = 1f; // Acceleration of movement
    public Transform bodyTransform;


    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the left mouse button was clicked
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object was hit by the raycast
                if (hit.transform == transform)
                {
                    // The object was hit, set it as selected
                    isSelected = true;
                    Debug.Log("Selected");
                }
                else
                {
                    // The object was not hit, deselect it
                    isSelected = false;
                    Debug.Log("Deselected");

                }
            }
        }

        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            // Check if the right mouse button was clicked and the object is selected
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                // Only hit objects on the "Terrain" layer
                // Get the point on the object's collider
                Vector3 point = hit.collider.ClosestPoint(hit.point);
                // Set the target position to move to
                targetPosition = point;
                Debug.Log($"Raying to X:{targetPosition.x}; Y:{targetPosition.y}; Z:{targetPosition.z}");
                Debug.DrawRay(Camera.main.transform.position, point, Color.blue);
            }
        }

        if (targetPosition != Vector3.zero)
        {
            // Move the object towards the target position
            bodyTransform.position = Vector3.SmoothDamp(bodyTransform.position, targetPosition, ref velocity, acceleration, speed);

            if (Vector3.Distance(bodyTransform.position, targetPosition) < 0.1f)
            {
                // If the distance between the object and the target position is small, stop moving
                targetPosition = Vector3.zero;
                velocity = Vector3.zero;
            }
        }
    }
}
