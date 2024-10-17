using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private float healthAmount = 100f;

    [SerializeField] private float timeSinceAttackedLimit = 0.5f;

    private float timeSinceAttacked = 0f;


    void Update()
    {
        
        if (healthAmount <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(3); //Lose scene
        }
    }


    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    // Method to heal the player by a certain amount
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Enemy")
        {
            
            if (Time.time > timeSinceAttacked)
            {
                timeSinceAttacked = Time.time + timeSinceAttackedLimit;
                Debug.Log("TAKING DAMAGE TO PLAYER");
                TakeDamage(20);
            }

            
            
        }
    }
}
