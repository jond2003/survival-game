using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHealth : MonoBehaviour
{
    [SerializeField] private float healthAmount = 30f;

    [SerializeField] private ItemDropper itemDropper;

    public void DamageAnimal(float damage)
    {
        healthAmount -= damage;
        if (healthAmount <= 0)
        {
            Debug.Log("animal killed");
            itemDropper.DropItem();
            Destroy(this.gameObject);
        }
    }
}
