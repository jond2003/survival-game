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

    private bool makeNewGrenade = true;

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
        if (!isPressed) Throw();
    }

    public void RMB_Action(bool isPressed)
    {
        if (!isPressed) Cook();
    }

    private void Throw()
    {
        if (makeNewGrenade)
        {
            grenade = Instantiate(fragGrenade, transform.parent.position, Quaternion.identity);
            explosiveGrenade = grenade.GetComponent<IGrenade>();

        }
        explosiveGrenade.Throw(playerCamera.transform.forward, explosionRadius, damage, cookTime);
        inventory.ConsumeHeldItem();

        makeNewGrenade = true;
    }

    private void Cook()
    {
        if (makeNewGrenade)
        {
            grenade = Instantiate(fragGrenade, transform.parent.position, Quaternion.identity);
            explosiveGrenade = grenade.GetComponent<IGrenade>();
            explosiveGrenade.Cook(explosionRadius, damage, cookTime);

            makeNewGrenade = false;

            inventory.ConsumeHeldItem();
        }
    }

    public void ReloadAction(bool isPressed) { }
}
