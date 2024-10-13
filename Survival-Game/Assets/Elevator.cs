using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform pointA;  // Ground floor
    public Transform pointB;  // First floor
    public float speed = 2f;  // Movement speed
    public bool isMoving = false;  // Control if the elevator is moving
    private GameObject player;
    private bool playerOnPlatform = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;  // Store the player reference
            playerOnPlatform = true;
            player.transform.parent = transform;  // Parent the player to the platform
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            player.transform.parent = null;  // Unparent when leaving
        }
    }

    public IEnumerator MoveTo(Transform targetPoint)
    {
        isMoving = true;  // Elevator starts moving

        while (Vector3.Distance(transform.position, targetPoint.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
            yield return null;  // Wait for the next frame
        }

        if (playerOnPlatform)
        {
            player.transform.parent = null;  // Unparent the player once arrived
            playerOnPlatform = false;
        }

        isMoving = false;  // Elevator stops moving
    }
}
