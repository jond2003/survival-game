using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    public float hungerDamage;
    public float hungerDamageRate;
    public float hungerDamageThreshold;

    public float hungerLossAmount;
    public float hungerLossRate;

    public float hungerHealAmount;
    public float hungerHealRate;
    public float hungerHealThreshold;
}
