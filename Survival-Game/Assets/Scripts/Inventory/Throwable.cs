using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private float throwForce = 15f;

    public void Throw(Vector3 direction)
    {
        Vector3 finalThrowForce = direction * throwForce;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.AddForce(finalThrowForce, ForceMode.Impulse);
    }
}
