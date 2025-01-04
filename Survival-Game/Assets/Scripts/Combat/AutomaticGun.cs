using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AutomaticGun : MonoBehaviour, IUsable
{
    [SerializeField] GunData gunData;

    private HUDManager hudManager;
    private GameObject gunInfoPanel;

    private TMP_Text ammoText;
    private TMP_Text totalAmmoText;
    private Slider reloadSlider;

    private float nextTimeToFire = 0f;
    private int bulletsInClip;
    private int ammoInInventory;
    private bool isReloading;
    private bool isFiring = false;

    private Camera playerCamera;
    private LayerMask layersToHit;
    private bool isInitialised = false;

    private AudioSource audioSource;
    private Animator animator;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void Initialise()
    {
        if (!isInitialised)
        {
            if (transform.parent != null)
            {
                playerCamera = transform.parent.parent.GetComponent<Camera>();
            }

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

            layersToHit = LayerMask.GetMask("NPC");

            bulletsInClip = gunData.clipSize;

            isInitialised = true;
        }

        if (isReloading)
        {
            isReloading = false;
            nextTimeToFire = Time.time;
        }

        gunInfoPanel.SetActive(true);
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.maxValue = gunData.reloadTime;
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
            ammoInInventory = PlayerInventory.Instance.HasItem(gunData.ammoType.itemName);
            if (isReloading)
            {
                // Update reload UI
                reloadSlider.value = Mathf.Clamp(reloadSlider.value + Time.deltaTime, 0, reloadSlider.maxValue);

                if (Time.time > nextTimeToFire)
                {
                    if (LoadAmmo())
                    {
                        UpdateAmmoText();
                        isReloading = false;
                        isFiring = false;
                        reloadSlider.gameObject.SetActive(false);
                    }
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
            nextTimeToFire = Time.time + 1f / gunData.fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        audioSource.Play();
        if (animator != null) animator.Play("Shoot", 0, 0f);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, gunData.range, layersToHit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target)
            {
                target.TakeDamage(gunData.damage);
            }
        }
        bulletsInClip -= 1;
        UpdateAmmoText();
    }

    private void Reload()
    {
        if (!isReloading && bulletsInClip < gunData.clipSize && ammoInInventory > 0)
        {
            nextTimeToFire = Time.time + gunData.reloadTime;

            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = 0f;

            isReloading = true;
        }
    }

    private bool LoadAmmo()
    {
        if (ammoInInventory <= 0) return false;

        int roundsReloaded = Mathf.Min(gunData.clipSize - bulletsInClip, ammoInInventory);
        PlayerInventory.Instance.ConsumeItem(gunData.ammoType.itemName, roundsReloaded);

        bulletsInClip += roundsReloaded;
        ammoInInventory = PlayerInventory.Instance.HasItem(gunData.ammoType.itemName);

        return true;
    }

    private void UpdateAmmoText()
    {
        totalAmmoText.text = ammoInInventory.ToString();
        ammoText.text = bulletsInClip + "/";
    }

    public void Uninitialise()
    {
        gunInfoPanel.gameObject.SetActive(false);
    }

    public void RMB_Action(bool isPressed) { }
}
