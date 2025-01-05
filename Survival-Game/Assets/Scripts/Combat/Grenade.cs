using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

interface IGrenade
{
    public void Throw(Vector3 throwDirection, float explosionRadius, float damage, float cookTime);
    public void Explode();
}

public class Grenade : MonoBehaviour, IUsable
{
    [SerializeField] private float damage = 60f;
    [SerializeField] private float explosionRadius = 8f;
    [SerializeField] private float cookTime = 3f;

    [SerializeField] private GameObject fragGrenade;

    private HUDManager hudManager;
    private GameObject grenadeInfoPanel;

    private TMP_Text grenadeCountText;

    private Camera playerCamera;
    private PlayerInventory inventory;
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

            hudManager = HUDManager.Instance;

            grenadeInfoPanel = hudManager.grenadeInfoPanel;

            foreach (Transform child in grenadeInfoPanel.transform)
            {
                switch (child.name)
                {
                    case "GrenadeCountText":
                        grenadeCountText = child.GetComponent<TMP_Text>();
                        break;
                }
            }

            isInitialised = true;
        }

        grenadeInfoPanel.SetActive(true);
        grenadeCountText.text = inventory.GetStackQuantity(inventory.hotbarIndex).ToString();
    }

    public void LMB_Action(bool isPressed)
    {
        if (!isPressed) Throw();
    }

    public void RMB_Action(bool isPressed) { }

    private void Throw()
    {
        GameObject grenade = Instantiate(fragGrenade, transform.parent.position, Quaternion.identity);
        explosiveGrenade = grenade.GetComponent<IGrenade>();
        //grenade.transform.localScale = Vector3.one;  // Make cooking grenade visible
        explosiveGrenade.Throw(playerCamera.transform.forward, explosionRadius, damage, cookTime);
        inventory.ConsumeHeldItem();

        grenadeCountText.text = inventory.GetStackQuantity(inventory.hotbarIndex).ToString();
    }

    public void Uninitialise()
    {
        grenadeInfoPanel.SetActive(false);
    }

    public void ReloadAction(bool isPressed) { }
}
