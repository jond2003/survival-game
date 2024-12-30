using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private FragmentationGrenade explosiveGrenade;

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
            grenade = Instantiate(fragGrenade, transform.parent);
            explosiveGrenade = grenade.GetComponent<FragmentationGrenade>();

            inventory.RemoveOneItem(inventory.hotbarIndex);
        }
        explosiveGrenade.Throw(playerCamera.transform.forward, explosionRadius, damage, cookTime);

        makeNewGrenade = true;
    }

    private void Cook()
    {
        if (makeNewGrenade)
        {
            grenade = Instantiate(fragGrenade, transform.parent);
            explosiveGrenade = grenade.GetComponent<FragmentationGrenade>();
            explosiveGrenade.Cook(explosionRadius, damage, cookTime);

            makeNewGrenade = false;

            inventory.RemoveOneItem(inventory.hotbarIndex);
        }
    }

    public void ReloadAction(bool isPressed) { }
}
