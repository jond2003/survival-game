using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform canvasPosition;
    [SerializeField] private GameObject highlightText;

    private PlayerInventory inventory;

    private bool isHighlighted = false;
    private GameObject highlightTextObject;

    void Start()
    {
        inventory = PlayerInventory.Instance;
    }

    public void Highlight(bool isOn)
    {
        if (isOn && !isHighlighted)
        {
            highlightTextObject = Instantiate(highlightText, canvasPosition.position, Quaternion.identity, canvasPosition);

            highlightTextObject.GetComponent<TMP_Text>().text = "Pick Up (E)";

            // Reset rotation to face the same direction as the parent canvas
            highlightTextObject.transform.localRotation = Quaternion.identity;

            isHighlighted = true;
        }
        else if (!isOn)
        {
            isHighlighted = false;
            Destroy(highlightTextObject);
        }
    }

    public void Interact()
    {
        inventory.StoreItem(gameObject);
        gameObject.SetActive(false);
    }
}
