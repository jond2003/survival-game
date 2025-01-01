using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public float health;
    public float movementSpeed;
    public float attackDamage;
    public float attackSpeed;
}
