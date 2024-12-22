using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenuManager : MonoBehaviour
{
    public InputActionReference FireRef, InteractRef, CrouchRef, SprintRef, JumpRef, MoveRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Disable all action references when the menu is enabled to prevent accidental input during rebind.
    private void OnEnable()
    {
        FireRef.action.Disable();
        InteractRef.action.Disable();
        CrouchRef.action.Disable();
        SprintRef.action.Disable();
        JumpRef.action.Disable();
        MoveRef.action.Disable();
    }

    // Re-enable all action references to restore player control.
    private void OnDisable()
    {
        FireRef.action.Enable();
        InteractRef.action.Enable();
        CrouchRef.action.Enable();
        SprintRef.action.Enable();
        JumpRef.action.Enable();
        MoveRef.action.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
