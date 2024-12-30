using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Food : MonoBehaviour, IUsable
{

    private PlayerInventory inventory;

    [SerializeField] private PlayerHunger playerHunger;

    void Start()
    {
        inventory = PlayerInventory.Instance;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHunger = player.GetComponent<PlayerHunger>();
        }

    }


    public void LMB_Action(bool isPressed)
    {
        if (isPressed ) Eat();
    }
    public void Initialise()
    {
        return;
    }  // Initialise to make usable

    public void RMB_Action(bool isPressed)
    {
        return;
    }   // Right Mouse Button Action
    public void ReloadAction(bool isPressed)
    {
        return;
    }  // Reload (R) Action


    void Eat()
    {
        playerHunger.IncreaseHunger(50);
        PlayerInventory.Instance.ConsumeHeldItem();
    }
}
