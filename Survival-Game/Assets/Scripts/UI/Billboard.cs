using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float offset = 0.5f;

    void Awake()
    {
        playerCamera = GameObject.FindWithTag("PlayerCamera");
    }

    void Update()
    {
        Vector3 parentPosition = transform.parent.position;

        // Radius of parent width and depth (x and z scale)
        float maxDistanceX = (transform.parent.localScale.x / 2f) + offset;
        float maxDistanceZ = (transform.parent.localScale.z / 2f) + offset;

        // Direction from the parent to the player's camera in xz plane
        Vector3 directionToCamera = (playerCamera.transform.position - parentPosition).normalized;
        directionToCamera.y = 0;

        // Set canvas position at max distance from the parent in xz plane
        Vector3 targetPosition = parentPosition + new Vector3(
            directionToCamera.x * maxDistanceX,
            0,
            directionToCamera.z * maxDistanceZ
        );

        transform.position = new Vector3(
            Mathf.Clamp(targetPosition.x, parentPosition.x - maxDistanceX, parentPosition.x + maxDistanceX),
            transform.position.y,
            Mathf.Clamp(targetPosition.z, parentPosition.z - maxDistanceZ, parentPosition.z + maxDistanceZ)
        );

        // Make the canvas face the player's camera
        transform.LookAt(
            transform.position + playerCamera.transform.rotation * Vector3.forward,
            playerCamera.transform.rotation * Vector3.up
        );
    }
}
