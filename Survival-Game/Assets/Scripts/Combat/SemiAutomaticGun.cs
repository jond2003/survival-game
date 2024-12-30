using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SemiAutomaticGun : MonoBehaviour, IUsable
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

    private Camera playerCamera;
    private LayerMask layersToHit;
    private bool isInitialised = false;

    public void Initialise()
    {
        if (!isInitialised)
        {
            if (transform.parent != null)
            {
                playerCamera = transform.parent.parent.GetComponent<Camera>();
            }

            layersToHit = LayerMask.GetMask("Default");

            bulletsInClip = clipSize;

            isInitialised = true;
        }

        if (isReloading)
        {
            isReloading = false;
            nextTimeToFire = Time.time;
        }

        UpdateAmmoText();
    }

    public void LMB_Action(bool isPressed)
    {
        if (isPressed) CheckShoot();
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
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.green);
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
            nextTimeToFire = Time.time + reloadTime;
            isReloading = true;
            Debug.Log("Reloading...");
        }
    }

    private void UpdateAmmoText()
    {
        ammoText.text = bulletsInClip + "/" + clipSize;
        if (bulletsInClip == 0)
        {
            ammoText.color = Color.red;
        }
        else
        {
            ammoText.color = Color.black;
        }
    }

    public void RMB_Action(bool isPressed) { }
}
