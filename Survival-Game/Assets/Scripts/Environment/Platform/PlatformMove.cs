using UnityEngine;
using System.Collections;


public class PlatformMove : MonoBehaviour
{
    public Transform pointA;  // Start point
    public Transform pointB;  // End point
    public float speed = 2f;  // Movement speed
    public float waitTime = 1f;  // Time to wait at each point

    private Transform targetPoint;
    private bool movingToPointB = true;
    private bool waiting = false;

    void Start()
    {
        targetPoint = pointB;  // Initially move towards point B
        StartCoroutine(MoveElevator());
    }

    IEnumerator MoveElevator()
    {
        while (true)
        {
            if (!waiting)
            {
                // Move the elevator towards the target point
                transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

                // Check if the elevator has reached the target point
                if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
                {
                    waiting = true;
                    yield return new WaitForSeconds(waitTime);  // Wait for a while

                    // Switch to the other point
                    if (movingToPointB)
                    {
                        targetPoint = pointA;
                    }
                    else
                    {
                        targetPoint = pointB;
                    }
                    movingToPointB = !movingToPointB;
                    waiting = false;
                }
            }
            yield return null;  // Wait until the next frame to continue moving
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Assuming the player has a "Player" tag
        {
            StartCoroutine(MoveElevator());
        }
    }
}
