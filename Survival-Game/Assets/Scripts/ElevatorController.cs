using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform pointA;  // Bottom point
    public Transform pointB;  // Top point
    public float speed = 2f;

    private bool movingUp = false; // I used this for debugging and want to keep it for a while longer
    private bool movingDown = false; // I used this for debugging and want to keep it for a while longer

    // Allowing the player to ride the elevator 
    private void OnTriggerEnter(Collider other)
    {
        // Player steps on
        if (other.CompareTag("Player"))
        {
            // Parenting the player to the platform when they step on it
            other.transform.SetParent(transform);
        }
    }

    // Player steps off
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Unparent the player when they step off the platform
            other.transform.SetParent(null);
        }
    }
    //=========================

    // Move elevator up to Point B
    public void MoveUp()
    {
        StopAllCoroutines();  // Stops any current movement
        StartCoroutine(MoveElevator(pointB.position)); // Does the moving up
        movingUp = true;
        movingDown = false;
    }

    // Move elevator to Point A (bottom)
    public void MoveDown()
    {
        StopAllCoroutines();  // Stops any current movement
        StartCoroutine(MoveElevator(pointA.position)); // Does the moving down
        movingUp = false;
        movingDown = true;
    }

    // "IEnumerator is a type used to define coroutines,
    // which allow you to write methods that can pause execution and resume later"
    private IEnumerator MoveElevator(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f) // As long as you're not there yet
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Move
            yield return null;  // Wait until next frame
        }
    }
}
