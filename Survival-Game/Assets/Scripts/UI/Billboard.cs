using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Billboard : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;


    void Awake()
    {
        playerCamera = GameObject.FindWithTag("PlayerCamera");
    }

    void Update()
    {
        // Make the canvas face the player's camera
        transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);
    }
}
