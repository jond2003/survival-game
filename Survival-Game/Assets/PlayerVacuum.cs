using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVacuum : MonoBehaviour
{
    [SerializeField] private float resourceSpeed = 5.0f;

    private Transform playerTransform;
    private Collider playerCollider;

    void Start()
    {
        playerTransform = transform.parent;
        
        Collider[] colliders = playerTransform.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
            {
                playerCollider = collider;
                break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Resource resource = other.GetComponent<Resource>();
        if (resource != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(other.transform.position, playerTransform.position, resourceSpeed * Time.deltaTime);

            if (playerCollider.bounds.Intersects(other.bounds))
            {
                resource.Collect();
            }
            else
            {
                other.transform.position = newPosition;
            }
        }
    }
}
