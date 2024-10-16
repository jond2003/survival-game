using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject damageText;
    [SerializeField] private Transform damageTextSpawnPosition;

    public void TakeDamage(float damage)
    {
        ShowDamage(damage);
    }

    // Creates TextMeshPro Object to temporarily display the amount of damage dealt to the target
    private void ShowDamage(float damage)
    {
        GameObject damageTextObject = Instantiate(damageText, damageTextSpawnPosition.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-0.5f, 1.0f), 0), Quaternion.identity, damageTextSpawnPosition);

        damageTextObject.GetComponent<TMP_Text>().text = damage.ToString();

        // Reset rotation to face the same direction as the parent canvas
        damageTextObject.transform.localRotation = Quaternion.identity;

        Destroy(damageTextObject, 0.5f);
    }
}
