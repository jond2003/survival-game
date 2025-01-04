using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHealth : MonoBehaviour
{
    [SerializeField] private float healthAmount = 30f;

    [SerializeField] private ItemDropper itemDropper;

    public bool DamageAnimal(float damage)
    {
        healthAmount -= damage;
        if (healthAmount <= 0)
        {
            itemDropper.DropItem();
            Destroy(this.gameObject);
        }

        return healthAmount <= 0;
    }

    public float GetHealth()
    {
        return healthAmount;
    }
}
