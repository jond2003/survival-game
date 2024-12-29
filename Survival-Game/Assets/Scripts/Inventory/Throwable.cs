using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private float throwForce = 15f;
    //[SerializeField] private float throwUpwardForce = 1f;

    public GameObject Throw(Vector3 direction)
    {
        Vector3 finalThrowForce = direction * throwForce;

        GameObject throwableClone = Instantiate(gameObject);
        throwableClone.transform.position = transform.position;

        Rigidbody rb = throwableClone.GetComponent<Rigidbody>();
        Collider col = throwableClone.GetComponent<Collider>();

        col.enabled = true;
        col.isTrigger = false;

        rb.useGravity = true;
        rb.AddForce(finalThrowForce, ForceMode.Impulse);

        return throwableClone;
    }
}
