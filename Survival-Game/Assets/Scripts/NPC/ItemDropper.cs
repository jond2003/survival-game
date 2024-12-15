using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private GameObject drop;

    public void DropItem()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
    }
}
