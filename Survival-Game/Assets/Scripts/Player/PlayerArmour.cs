using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmour : MonoBehaviour
{

    public static PlayerArmour instance { get; private set; }


    [SerializeField] private float currentDamageMitigation;
    [SerializeField] private int currentArmourLevel;
    [SerializeField] private int maxArmourLevel = 5;


    private List<float> damageMitigationPerLevel = new List<float> //maps damage mitgation to index, e.g. level 1 = 1f
    {
        1.0f, 
        0.90f, 
        0.85f,  
        0.75f, 
        0.65f,   
    };

    private List<int> steelPerLevel = new List<int> //steel required per level.
    {
        3,
        5,
        7,
        9
    };

    private void Start()
    {
        ResetArmour();
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
            currentDamageMitigation = damageMitigationPerLevel[currentArmourLevel - 1]; //-1 because lists start at 0 

        }

    }

   
    public int GetArmourLevel()
    {
        return currentArmourLevel;
    }
    public float GetDamageMitigation()
    {
        return currentDamageMitigation;
    }
    public void ResetArmour()
    {
        currentArmourLevel = 1;
        currentDamageMitigation = damageMitigationPerLevel[0];
    }
}
