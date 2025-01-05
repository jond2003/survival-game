using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthAmount = 100f;


    [SerializeField] private TextMeshProUGUI deadText;

    void Update()
    {

        if (healthAmount <= 0)
        {
           
            StartCoroutine(ShowDeathScreen());
        }
    }


    public void TakeDamage(float damage)
    {
        float damageMitigationFromArmour;
        if (damage > 0) //healing uses negative number, and does not use damage mitigation
        {
            damageMitigationFromArmour = PlayerArmour.instance.GetDamageMitigation();
        }
        else
        {
            damageMitigationFromArmour = 1;
        }
        healthAmount -= (damage * damageMitigationFromArmour);
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    // Method to heal the player by a certain amount
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    public float GetCurrentHealth()
    {
        return healthAmount;
    }

    public void KillPlayer()
    {
        healthAmount = 0;
    }

    private IEnumerator ShowDeathScreen()
    {

        //disable player movement and looking
        Cursor.lockState = CursorLockMode.None;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;

        }

        Camera playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            PlayerLook playerLook = playerCamera.GetComponent<PlayerLook>();
            if (playerLook != null)
            {
                playerLook.enabled = false;
            }
        }
        if (deadText != null)
        {
            deadText.gameObject.SetActive(true); //show player died text    
        }


        float showTime = 0f;

        while (showTime < 3) //show death screen for this many secs
        {
            showTime += Time.deltaTime;
            yield return null;

        }
        SceneManager.LoadScene("LoseScene"); //Lose scene

    }

}
