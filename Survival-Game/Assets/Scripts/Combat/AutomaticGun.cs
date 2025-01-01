using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AutomaticGun : MonoBehaviour, IUsable
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float impactForce = 25f;
    [SerializeField] private int clipSize = 30;
    [SerializeField] private float reloadTime = 1f;

    private HUDManager hudManager;
    private GameObject gunInfoPanel;

    private TMP_Text ammoText;
    private TMP_Text totalAmmoText;
    private Slider reloadSlider;

    private float nextTimeToFire = 0f;
    private int bulletsInClip;
    private bool isReloading;
    private bool isFiring = false;

    private Camera playerCamera;
    private LayerMask layersToHit;
    private bool isInitialised = false;

    void Awake()
    {
        hudManager = HUDManager.Instance;
        gunInfoPanel = hudManager.gunInfoPanel;

        foreach (Transform child in gunInfoPanel.transform)
        {
            switch (child.name)
            {
                case "Reload Slider":
                    reloadSlider = child.GetComponent<Slider>();
                    break;
                case "TotalAmmoText":
                    totalAmmoText = child.GetComponent<TMP_Text>();
                    break;
                case "AmmoText":
                    ammoText = child.GetComponent<TMP_Text>();
                    break;
            }
        }
    }

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

        gunInfoPanel.SetActive(true);
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.maxValue = reloadTime;
        reloadSlider.gameObject.SetActive(false);

        UpdateAmmoText();
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
            if (isReloading)
            {
                // Update reload UI
                reloadSlider.value = Mathf.Clamp(reloadSlider.value + Time.deltaTime, 0, reloadSlider.maxValue);

                if (Time.time > nextTimeToFire)
                {
                    bulletsInClip = clipSize;
                    UpdateAmmoText();
                    isReloading = false;
                    isFiring = false;
                    reloadSlider.gameObject.SetActive(false);
                }
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
            nextTimeToFire = Time.time + reloadTime;

            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = 0f;

            isReloading = true;
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

    public void Uninitialise()
    {
        gunInfoPanel.gameObject.SetActive(false);
    }

    public void RMB_Action(bool isPressed) { }
}
