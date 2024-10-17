using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float healthAmount = 50f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void damageEnemy(float damage)
    {
        healthAmount -= damage;
        if (healthAmount <= 0)
        {
            Debug.Log("Enemy killed");
            Destroy(this.gameObject);
        }

    }
}
