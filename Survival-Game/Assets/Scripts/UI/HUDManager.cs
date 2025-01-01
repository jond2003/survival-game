using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameObject gunInfoPanel;
    public GameObject grenadeInfoPanel;

    public static HUDManager Instance { get; private set; }
    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        gunInfoPanel = transform.Find("GunInfo").gameObject;
        grenadeInfoPanel = transform.Find("GrenadeInfo").gameObject;
    }
}
