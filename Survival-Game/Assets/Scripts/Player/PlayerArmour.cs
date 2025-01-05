using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerArmour : MonoBehaviour
{

    public static PlayerArmour instance { get; private set; }


    [SerializeField] private float currentDamageMitigation;
    [SerializeField] private int currentArmourLevel;
    [SerializeField] private int maxArmourLevel = 3;

    [SerializeField] TMP_Text armourText;

    [SerializeField] CraftingRecipeData armourRecipeData;


    private List<float> damageMitigationPerLevel = new List<float> //maps damage mitgation to index, e.g. level 1 = 1f
    {
        1.0f, 
        0.90f, 
        0.85f,
        0.80f,
    };


    private void Start()
    {
        ResetArmour();
        UpdateArmourText();
    }
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void UpgradeArmourLevel()
    {
        if (currentArmourLevel < maxArmourLevel)
        {
            currentArmourLevel += 1;
            currentDamageMitigation = damageMitigationPerLevel[currentArmourLevel]; //-1 because lists start at 0 
            UpdateArmourText();

        }

    }



   
    public int GetArmourLevel()
    {
        return currentArmourLevel;
    }

    public int GetMaxArmourLevel()
    {
        return maxArmourLevel;
    }
    public float GetDamageMitigation()
    {
        return currentDamageMitigation;
    }
    private void ResetArmour()
    {
        currentArmourLevel = 0; //no armour
        currentDamageMitigation = damageMitigationPerLevel[0];
    }

    private void UpdateArmourText()
    {
        if (armourText == null) return;
        armourText.text = currentArmourLevel + "/3";
    }

    


}
