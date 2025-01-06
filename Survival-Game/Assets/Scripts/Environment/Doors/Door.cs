using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOpen = false;

    private Vector3 closedRotation; // Used for rotating doors
    private Vector3 openRotation;
    private Vector3 closedPosition; // Used for sliding doors
    private Vector3 openPosition;

    [SerializeField] private DoorType doorType = DoorType.Rotating;
    [SerializeField] private float openAngle = 90f; // Angle the door rotates to opening (for rotating doors)
    [SerializeField] private float slideDistance = 2f; // Distance the door slides when opening (for sliding doors)
    [SerializeField] private float openSpeed = 2f;

    public enum DoorType
    {
        Rotating,
        Sliding
    }

    void Start()
    {
        // Initialize the door's closed and open states based on its type
        if (doorType == DoorType.Rotating)
        {
            closedRotation = transform.eulerAngles; // Closed rotation is the current rotation
            openRotation = closedRotation + new Vector3(0, openAngle, 0); // Open rotation is found by adding the open angle
        }
        else if (doorType == DoorType.Sliding)
        {
            closedPosition = transform.position; // Closed position is the current position
            openPosition = closedPosition + transform.right * slideDistance; // Open position found by moving along the right axis
        }

        if (isOpen)
        {
            transform.eulerAngles = openRotation;
            transform.position = openPosition;
        }
    }

    // When interacting with door
    public void Interact()
    {
        if (!isOpen)
        {
            StopAllCoroutines(); // Stop ongoing door animations
            StartCoroutine(OpenDoor()); // Start coroutine to open door
        }
        else
        {
            StopAllCoroutines(); // Stop ongoing door animations
            StartCoroutine(CloseDoor()); // Start coroutine to open door
        }
    }

    public void Highlight(bool isOn)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Change the material color based on whether the door is highlighted
            Color color = isOn ? Color.yellow : Color.white;
            renderer.material.color = color;
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpen = true;
        float elapsed = 0f; // Tracks the elapsed time for the animation

        // Rotate rotating/ normal door
        if (doorType == DoorType.Rotating)
        {
            while (elapsed < 1f) // Continue until the animation is complete
            {
                // Smoothly interpolate the door's rotation from closed to open
                transform.eulerAngles = Vector3.Lerp(closedRotation, openRotation, elapsed);
                elapsed += Time.deltaTime * openSpeed;
                yield return null;
            }
            transform.eulerAngles = openRotation; // Ensure the door's final rotation is set to the open position
        }
        // Position change for sliding doors
        else if (doorType == DoorType.Sliding)
        {
            while (elapsed < 1f) // Continue until the animation is complete
            {
                // Smoothly interpolate the door's position from closed to open
                transform.position = Vector3.Lerp(closedPosition, openPosition, elapsed);
                elapsed += Time.deltaTime * openSpeed;
                yield return null;
            }
            transform.position = openPosition; // Ensure the door's final position is set to the open position
        }
    }

    private IEnumerator CloseDoor()
    {
        isOpen = false;
        float elapsed = 0f; // Tracks the elapsed time for the animation

        // Rotate rotating/ normal door
        if (doorType == DoorType.Rotating)
        {
            while (elapsed < 1f) // Continue until the animation is complete
            {
                // Smoothly interpolate the door's rotation from open to closed
                transform.eulerAngles = Vector3.Lerp(openRotation, closedRotation, elapsed);
                elapsed += Time.deltaTime * openSpeed;
                yield return null;
            }
            transform.eulerAngles = closedRotation; // Ensure the door's final rotation is set to the closed position
        }
        // Position change for sliding doors
        else if (doorType == DoorType.Sliding)
        {
            while (elapsed < 1f) // Continue until the animation is complete
            {
                // Smoothly interpolate the door's position from open to closed
                transform.position = Vector3.Lerp(openPosition, closedPosition, elapsed);
                elapsed += Time.deltaTime * openSpeed;
                yield return null;
            }
            transform.position = closedPosition; // Ensure the door's final position is set to the closed position
        }
    }
}
