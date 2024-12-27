using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, IUsable
{
    [SerializeField] private InputAction inputAxis;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float impactForce = 25f;

    private float nextTimeToFire = 0f;

    private Camera playerCamera;

    private LayerMask layersToHit;

    public void Initialise()
    {
        if (transform.parent != null)
        {
            playerCamera = transform.parent.parent.GetComponent<Camera>();
        }
        inputAxis = playerInput.actions.FindAction("Fire");

        layersToHit = LayerMask.GetMask("Default");
    }

    void Update()
    {
        if (playerCamera != null)
        {
            CheckShoot();
        }
    }

    private void CheckShoot()
    {
        if (inputAxis.IsPressed() && Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, layersToHit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
