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

    [SerializeField] private float damageToPlayerForHungerRate = 4f;
    [SerializeField] private float damageToPlayerForHunger = 5f; //deal damage to player when under hunger threshhold
    

    [SerializeField] float hungerLossRate = 5.0f; //time between each decrement of hunger
    [SerializeField] float hungerLossPerDecrement = 2.0f; //How much hunger loss per decrement



    [SerializeField] float healthIncreaseRate = 5.0f; 
    [SerializeField] float healthIncreasePerIncrement = -4.0f;  //minus because uses takedamage function

    [SerializeField] PlayerHealth playerHealth;


    Coroutine healthDecreaseCoroutine;
    Coroutine healthIncreaseCoroutine;


    void Start()
    {
        StartCoroutine(DecrementHunger());
    }


    void Update()
    {
        if (Input.GetKeyDown("l")) //JUST FOR TESTING, REMOVE AFTER
        {
            Debug.Log("HUNGER INCREASE");
            IncreaseHunger(20);
        }
    }

    private IEnumerator DecrementHunger()
    {
        while (true)
        {

            hungerAmount -= hungerLossPerDecrement;

            if (hungerAmount <= 0 && healthDecreaseCoroutine == null) //minimum hunger is 0
            {
                healthDecreaseCoroutine = StartCoroutine(DecrementHealthDueToHunger());
                hungerAmount = 0;
            } else if (hungerAmount <= 0)
            {
                hungerAmount = 0;
            } else if (hungerAmount >= 70 && healthIncreaseCoroutine == null) // heal player incremently
            {
                healthIncreaseCoroutine = StartCoroutine(IncreaseHealthDueToHunger());
            } else if (hungerAmount < 70 && healthIncreaseCoroutine != null) // stop heal player incremently
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

        if (hungerAmount > 0 && healthDecreaseCoroutine != null)
        {
            StopCoroutine(healthDecreaseCoroutine);
            healthDecreaseCoroutine = null;

        }

        hungerBar.fillAmount = hungerAmount / 100f;
    }
}
