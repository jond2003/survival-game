using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float healthAmount = 50f;

    [SerializeField] private ItemDropper itemDropper;

    public void DamageEnemy(float damage)
    {
        healthAmount -= damage;
        Debug.Log(healthAmount + " health rn");
        if (healthAmount <= 0)
        {
            Debug.Log("Enemy killed");
            itemDropper.DropItem();
            Destroy(this.gameObject);
        }
    }
}
