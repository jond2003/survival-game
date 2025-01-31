using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject damageText;
    [SerializeField] private Transform damageTextSpawnPosition;

    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private AnimalHealth animalHealth;

    public void TakeDamage(float damage)
    {
        DamageNPC(damage);
    }

    
    private void ShowDamage(float damage)
    {
        GameObject damageTextObject = Instantiate(this.damageText, damageTextSpawnPosition.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-0.5f, 1.0f), 0), Quaternion.identity, damageTextSpawnPosition);

        TMP_Text damageText = damageTextObject.GetComponent<TMP_Text>();
        damageText.text = damage.ToString();

 
        damageTextObject.transform.localRotation = Quaternion.identity;

        damageTextObject.transform.SetParent(null);
        //Needed because gets detached from parent
        damageTextObject.transform.position = damageTextSpawnPosition.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-0.5f, 1.0f), 0);

        //To make sure text isnt stretched based on the parent object scale
        damageText.text = damage.ToString();
        damageTextObject.transform.localScale = Vector3.one; 
        damageText.fontSize = 0.4f; 
       
        damageTextObject.transform.SetParent(damageTextSpawnPosition);

        Destroy(damageTextObject, 0.5f);
    }

    private void DamageNPC(float damage)
    {
        bool isDead = false;
        if (enemyHealth != null)
        {
            isDead = enemyHealth.GetHealth() <= 0;
            enemyHealth.DamageEnemy(damage);
        }
        else if (animalHealth != null)
        {
            isDead = animalHealth.GetHealth() <= 0;
            animalHealth.DamageAnimal(damage);
        }
        if (!isDead) ShowDamage(damage);
    }
}
