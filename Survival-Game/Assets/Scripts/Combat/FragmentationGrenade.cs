using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentationGrenade : MonoBehaviour, IUsable
{
    [SerializeField] private float damage = 50f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float cookTime = 3f;

    private bool isCooking = false;
    private float explodeTime = 0f;

    private Camera playerCamera;
    private Throwable throwable;
    private PlayerInventory inventory;
    private GameObject thrownItem;

    private bool isInitialised = false;

    void Start()
    {
        throwable = GetComponent<Throwable>();
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

    void Update()
    {
        if (isInitialised)
        {
            if (isCooking) CheckExplode();
        }
    }

    private void Throw()
    {
        if (!isCooking) Cook();
        thrownItem = throwable.Throw(playerCamera.transform.forward);
    }

    private void Cook()
    {
        isCooking = true;
        explodeTime = Time.time + cookTime;
    }

    private void CheckExplode()
    {
        if (Time.time > explodeTime) Explode();
    }

    private void Explode()
    {
        isCooking = false;
        Debug.Log("Exploded at " + transform.position);
        
        if (thrownItem != null)
        {
            Destroy(thrownItem);
        }
    }

    public void ReloadAction(bool isPressed) { }
}
