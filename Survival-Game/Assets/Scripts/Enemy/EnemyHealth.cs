using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float healthAmount = 50f;

    [SerializeField] private ItemDropper itemDropper;

    [SerializeField] Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DamageEnemy(float damage)
    {
        healthAmount -= damage;
        //Debug.Log(healthAmount + " health rn");
        if (healthAmount <= 0)
        {
            StartCoroutine(DyingAnimation());

        }
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
