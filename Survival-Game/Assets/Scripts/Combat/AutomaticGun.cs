using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutomaticGun : MonoBehaviour, IUsable
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float impactForce = 25f;
    [SerializeField] private int clipSize = 30;
    [SerializeField] private float reloadTime = 1f;

    [SerializeField] private TMP_Text ammoText;

    private float nextTimeToFire = 0f;
    private int bulletsInClip;
    private bool isReloading;
    private bool isFiring = false;

    private Camera playerCamera;
    private LayerMask layersToHit;
    private bool isInitialised = false;

    public void Initialise()
    {
        if (transform.parent != null)
        {
            playerCamera = transform.parent.parent.GetComponent<Camera>();
        }

        layersToHit = LayerMask.GetMask("Default");

        bulletsInClip = clipSize;
        UpdateAmmoText();

        isInitialised = true;
    }

    public void LMB_Action(bool isPressed)
    {
        isFiring = isPressed;
    }

    public void ReloadAction(bool isPressed)
    {
        Reload();
    }

    void Update()
    {
        if (isInitialised)
        {
            if (isReloading && Time.time > nextTimeToFire)
            {
                Debug.Log("Finished reloading...");
                bulletsInClip = clipSize;
                UpdateAmmoText();
                isReloading = false;
                isFiring = false;
            }

            if (isFiring)
            {
                CheckShoot();
            }
        }
    }

    private void CheckShoot()
    {
        if (Time.time > nextTimeToFire && bulletsInClip > 0)
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
        bulletsInClip -= 1;
        UpdateAmmoText();
    }

    private void Reload()
    {
        if (!isReloading && bulletsInClip < clipSize)
        {
            nextTimeToFire += reloadTime;
            isReloading = true;
            Debug.Log("Reloading...");
        }
    }

    private void UpdateAmmoText()
    {
        ammoText.text = bulletsInClip + "/" + clipSize;
    }

    public void RMB_Action(bool isPressed) { }
}
