using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunGun : MonoBehaviour, IUsable
{
    [SerializeField] GunData gunData;

    private HUDManager hudManager;
    private GameObject gunInfoPanel;

    private TMP_Text ammoText;
    private TMP_Text totalAmmoText;
    private Slider reloadSlider;

    private float nextTimeToFire = 0f;
    private int ammoInInventory;
    private int bulletsInClip;
    private bool isReloading;

    private Camera playerCamera;
    private LayerMask layersToHit;
    private bool isInitialised = false;
    private bool isActive = false;

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

        isActive = true;
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
            ammoInInventory = PlayerInventory.Instance.HasItem(gunData.ammoType.itemName);
            if (isReloading)
            {
                // Update reload UI
                reloadSlider.value = Mathf.Clamp(reloadSlider.value + Time.deltaTime, 0, reloadSlider.maxValue);

                if (Time.time > nextTimeToFire)
                {
                    if (LoadAmmo())
                    {
                        isReloading = false;
                        reloadSlider.gameObject.SetActive(false);
                    }
                }
            }

            if (isActive)
            {
                UpdateAmmoText();
            }
        }
    }

    private void CheckShoot()
    {
        if (Time.time > nextTimeToFire && bulletsInClip > 0)
        {
            if (gunData.fireRate > 0)
            {
                nextTimeToFire = Time.time + 1f / gunData.fireRate;
            }
            Shoot();
        }
    }

    private void Shoot()
    {
        audioSource.Play();
        if (animator != null) animator.Play("Shoot", 0, 0f);

        for (int i = 0; i < gunData.pelletsCount; i++)
        {
            float randomX = (Random.Range(-gunData.spreadRadius, gunData.spreadRadius) + Random.Range(-gunData.spreadRadius, gunData.spreadRadius)) / 2;
            float randomY = (Random.Range(-gunData.spreadRadius, gunData.spreadRadius) + Random.Range(-gunData.spreadRadius, gunData.spreadRadius)) / 2;
            Vector3 pelletDirection = new Vector3(playerCamera.transform.forward.x + randomX, playerCamera.transform.forward.y + randomY, playerCamera.transform.forward.z);
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, pelletDirection, out hit, gunData.range, layersToHit))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target)
                {
                    target.TakeDamage(Mathf.Ceil(gunData.damage / gunData.pelletsCount));
                }
            }
        }
        bulletsInClip -= 1;
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
        isActive = false;
    }

    public void RMB_Action(bool isPressed) { }
}
