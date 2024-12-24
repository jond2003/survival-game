using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    private float dropYaxis = 1.5f;

    public void DropItem()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            Vector3 ground = hit.point;
            Vector3 newPosition = transform.position;

            newPosition.y = ground.y + dropYaxis; //fixed axis on y

            Instantiate(drop, newPosition, Quaternion.identity);
        }
    }
}
