using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public record ItemDrop
{
    public Resource drop;
    [Range(1, 10)] public int maxDrops = 1;
    [Range(0, 1)] public float initialDropChance = 1f;  // Probability of 1 item being dropped
    [Range(1, 10)] public float chanceDivisor = 2f;  // Amount that the chance is divided by after each drop
    [Range(1, 8)] public float exponentStep = 1f;  // Increase in the amount that the divisor is raised to after each drop
}

public class ItemDropper : MonoBehaviour
{
    private List<ItemDrop> drops = new List<ItemDrop>();
    private float dropYaxis = 1.5f;

    [SerializeField] private ItemDropperData easyItemDropperData;
    [SerializeField] private ItemDropperData hardItemDropperData;
    [SerializeField] private ItemDropperData impossibleItemDropperData;
    private ItemDropperData itemDropperData;

    public void Start()
    {
        itemDropperData = (ItemDropperData)GameSettingsManager.GetDifficultyData(easyItemDropperData, hardItemDropperData, impossibleItemDropperData);
        drops = itemDropperData.drops;
    }

    public void DropItem()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            Vector3 ground = hit.point;
            Vector3 newPosition = transform.position;

            newPosition.y = ground.y + dropYaxis; //fixed axis on y

            foreach (ItemDrop drop in drops)
            {
                int numDrops = Gamble(drop);

                // Instantiate all won items
                for (int i = 0; i < numDrops; i++)
                {
                    Instantiate(drop.drop.gameObject, newPosition + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.identity);
                }
            }
        }
    }

    // Determines the number of items to drop
    // Generates a random number between 0 and 1, checks if this number is within the dropChance, grants the player a drop if it is
    // Responsible gambling since it stops playing at the first loss
    private int Gamble(ItemDrop drop)
    {
        int numDrops = 0;

        float currentDropChance = drop.initialDropChance;
        float exponent = 1f;

        bool winning = true;
        while (winning && numDrops < drop.maxDrops)
        {
            float randomNum = Random.Range(0f, 1f);
            if (randomNum <= currentDropChance)
            {
                numDrops++;
            }
            else
            {
                 winning = false;
            }
            currentDropChance /= Mathf.Pow(drop.chanceDivisor, exponent);
            exponent += drop.exponentStep;
        }

        return numDrops;
    }
}
