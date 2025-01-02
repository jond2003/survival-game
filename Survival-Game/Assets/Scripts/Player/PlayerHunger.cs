using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHunger : MonoBehaviour
{

    [SerializeField] private Image hungerBar;
    [SerializeField] private float hungerAmount = 100f;

    private float damageToPlayerForHungerRate = 4f;
    private float damageToPlayerForHunger = 5f; //deal damage to player when under hunger threshhold
    

    private float hungerLossRate = 5.0f; //time between each decrement of hunger
    private float hungerLossPerDecrement = 2.0f; //How much hunger loss per decrement

    private float healthIncreaseRate = 5.0f; 
    private float healthIncreasePerIncrement = -4.0f;  //minus because uses takedamage function

    [SerializeField] private PlayerData easyPlayerData;
    [SerializeField] private PlayerData hardPlayerData;
    [SerializeField] private PlayerData impossiblePlayerData;
    private PlayerData playerData;

    [SerializeField] PlayerHealth playerHealth;


    Coroutine healthDecreaseCoroutine;
    Coroutine healthIncreaseCoroutine;


    void Start()
    {
        playerData = (PlayerData)GameSettingsManager.GetDifficultyData(easyPlayerData, hardPlayerData, impossiblePlayerData);

        damageToPlayerForHunger = playerData.hungerDamageRate;
        damageToPlayerForHunger = playerData.hungerDamage;
        hungerLossRate = playerData.hungerLossRate;
        hungerLossPerDecrement = playerData.hungerLossAmount;
        healthIncreaseRate = playerData.hungerHealRate;
        healthIncreasePerIncrement = playerData.hungerHealAmount;

        StartCoroutine(DecrementHunger());
    }

    private IEnumerator DecrementHunger()
    {
        while (true)
        {

            hungerAmount -= hungerLossPerDecrement;

            if (hungerAmount < 30  && hungerAmount > 0 && healthDecreaseCoroutine == null) //minimum hunger is 0
            {
                healthDecreaseCoroutine = StartCoroutine(DecrementHealthDueToHunger());

            } else if (hungerAmount <= 0) //Player dies when hunger is 0
            {
                playerHealth.KillPlayer();
            } else if (hungerAmount >= playerData.hungerHealThreshold && healthIncreaseCoroutine == null) // heal player incremently
            {
                healthIncreaseCoroutine = StartCoroutine(IncreaseHealthDueToHunger());
            } else if (hungerAmount < playerData.hungerHealThreshold && healthIncreaseCoroutine != null) // stop heal player incremently
            {
                StopCoroutine(healthIncreaseCoroutine);
                healthIncreaseCoroutine = null;
            }
            hungerBar.fillAmount = hungerAmount / 100f;

            yield return new WaitForSeconds(hungerLossRate);
        }
    }

    private IEnumerator DecrementHealthDueToHunger()
    {
        while (true)
        {
            playerHealth.TakeDamage(damageToPlayerForHunger);
            yield return new WaitForSeconds(damageToPlayerForHungerRate);
        }
    }

    private IEnumerator IncreaseHealthDueToHunger()
    {
        while (true)
        {
            playerHealth.TakeDamage(healthIncreasePerIncrement);
            yield return new WaitForSeconds(healthIncreaseRate);
        }
    }

    public void IncreaseHunger(float hungerAmountIncrease)
    {
        hungerAmount += hungerAmountIncrease;
        hungerAmount = Mathf.Clamp(hungerAmount, 0, 100);

        if (hungerAmount > playerData.hungerDamageThreshold && healthDecreaseCoroutine != null)
        {
            StopCoroutine(healthDecreaseCoroutine);
            healthDecreaseCoroutine = null;

        }

        hungerBar.fillAmount = hungerAmount / 100f;
    }
}
