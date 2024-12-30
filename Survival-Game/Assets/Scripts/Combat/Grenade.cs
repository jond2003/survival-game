using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGrenade
{
    public void Throw(Vector3 throwDirection, float explosionRadius, float damage, float cookTime);
    public void Cook(float explosionRadius, float damage, float cookTime);
    public void Explode();
}

public class Grenade : MonoBehaviour, IUsable
{
    [SerializeField] private float damage = 50f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float cookTime = 3f;

    [SerializeField] private GameObject fragGrenade;

    private bool isCooking = false;
    private float explosionTime = 0f;

    private Camera playerCamera;
    private PlayerInventory inventory;
    private GameObject grenade;
    private IGrenade explosiveGrenade;

    private bool isInitialised = false;

    void Start()
    {
        inventory = PlayerInventory.Instance;
    }

    public void Initialise()
    {
        if (!isInitialised)
        {
            if (transform.parent != null)
            {
                playerCamera = transform.parent.parent.GetComponent<Camera>();
            }

            isInitialised = true;
        }
    }

    public void LMB_Action(bool isPressed)
    {
        if (!isPressed && !isCooking) Throw();
    }

    public void RMB_Action(bool isPressed)
    {
        if (isPressed) Cook();  // Press and hold
        if (!isPressed && isCooking) Throw();  // Release
    }

    void Update()
    {
        if (isInitialised)
        {
            if (isCooking)
            {
                grenade.transform.localScale = Vector3.zero;  // Make unthrown cooking grenade invisible
                if (Time.time >= explosionTime)
                {
                    grenade.transform.localScale = Vector3.one;  // Make cooking grenade visible
                    inventory.ConsumeHeldItem();
                }
            }
        }
    }

    private void Throw()
    {
        if (!isCooking)
        {
            grenade = Instantiate(fragGrenade, transform.parent.position, Quaternion.identity);
            explosiveGrenade = grenade.GetComponent<IGrenade>();
        }
        grenade.transform.localScale = Vector3.one;  // Make cooking grenade visible
        explosiveGrenade.Throw(playerCamera.transform.forward, explosionRadius, damage, cookTime);
        inventory.ConsumeHeldItem();

        isCooking = false;
    }

    private void Cook()
    {
        if (!isCooking)
        {
            grenade = Instantiate(fragGrenade, transform.parent.position, Quaternion.identity);
            explosiveGrenade = grenade.GetComponent<IGrenade>();
            explosiveGrenade.Cook(explosionRadius, damage, cookTime);

            explosionTime = Time.time + cookTime;
            isCooking = true;
        }
    }

    public void ReloadAction(bool isPressed) { }
}
