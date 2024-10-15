using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject damageText;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0);

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        ShowDamage(damage);
    }

    public void ShowDamage(float damage)
    {
        // Instantiate the damage text at the spawn position + offset
        GameObject damageTextObject = Instantiate(damageText, spawnPosition.position + offset, Quaternion.identity);

        // Get the TextMeshPro or Text component and set the damage text
        damageTextObject.GetComponent<Text>().text = damage.ToString();

        // Optionally, destroy the text after a delay (e.g., 1.5 seconds)
        Destroy(damageTextObject, 5.5f);
    }
}
