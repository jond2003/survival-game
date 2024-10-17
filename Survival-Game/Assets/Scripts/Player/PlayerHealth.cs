using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if health is zero or below, indicating the game is over
        if (healthAmount <= 0)
        {
            Debug.Log("Game Over"); // Output "Game Over" to the console
        }

        // If the Return key is pressed, deal 20 damage to the player
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("take damage");
            TakeDamage(20);
        }

        // If the Space key is pressed, heal the player by 5 points
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("heal");
            Heal(5);
        }
    }

    // Method to decrease the player's health by a certain damage amount
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
}
