using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Food : MonoBehaviour, IUsable
{
    [SerializeField] private PlayerHunger playerHunger;

    [SerializeField] private FoodData easyFoodData;
    [SerializeField] private FoodData hardFoodData;
    [SerializeField] private FoodData impossibleFoodData;

    private FoodData foodData;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHunger = player.GetComponent<PlayerHunger>();
        }

        foodData = (FoodData)GameSettingsManager.GetDifficultyData(easyFoodData, hardFoodData, impossibleFoodData);
    }

    public void LMB_Action(bool isPressed)
    {
        if (!isPressed) Eat();
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
        playerHunger.IncreaseHunger(foodData.hungerIncrease);
        PlayerInventory.Instance.ConsumeHeldItem();
    }

    public void Uninitialise() { }
}
