using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    private float healthAmount = 50f;

    [SerializeField] private ItemDropper itemDropper;

    [SerializeField] Animator animator;

    [SerializeField] private EnemyData easyEnemyData;
    [SerializeField] private EnemyData hardEnemyData;
    [SerializeField] private EnemyData impossibleEnemyData;
    private EnemyData enemyData;

    private void Start()
    {
        animator = GetComponent<Animator>();

        enemyData = (EnemyData)GameSettingsManager.GetDifficultyData(easyEnemyData, hardEnemyData, impossibleEnemyData);

        healthAmount = enemyData.health;
    }

    public bool DamageEnemy(float damage)
    {
        healthAmount -= damage;
        //Debug.Log(healthAmount + " health rn");
        if (healthAmount <= 0)
        {
            StartCoroutine(DyingAnimation());

        }
        return healthAmount <= 0;
    }


    private IEnumerator DyingAnimation()
    {
        animator.SetBool("isDead", true);
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(2f);
        itemDropper.DropItem();
        Destroy(this.gameObject);
    }
}
