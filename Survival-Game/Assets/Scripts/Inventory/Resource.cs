using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform canvasPosition;
    [SerializeField] private GameObject highlightText;

    public int maxStack = 1;
    public string itemName;

    public Sprite sprite;

    private PlayerInventory inventory;

    private bool isHighlighted = false;
    private GameObject highlightTextObject;

    void Start()
    {
        inventory = PlayerInventory.Instance;

        if (itemName == null)
        {
            itemName = gameObject.name;
        }
    }

    public void Highlight(bool isOn)
    {
        if (isOn && !isHighlighted)
        {
            highlightTextObject = Instantiate(highlightText, canvasPosition.position, Quaternion.identity, canvasPosition);

            TMP_Text highlightTextComponent = highlightTextObject.GetComponent<TMP_Text>();

            // Reset rotation to face the same direction as the parent canvas
            highlightTextObject.transform.localRotation = Quaternion.identity;

            highlightTextObject.transform.SetParent(null);
            //Needed because gets detached from parent
            highlightTextObject.transform.position = canvasPosition.position + new Vector3(0.0f, gameObject.transform.localScale.y / 2, 0.0f);

            //To make sure text isnt stretched based on the parent object scale
            highlightTextComponent.text = "Pick Up (E)";
            highlightTextObject.transform.localScale = Vector3.one;
            highlightTextComponent.fontSize = 0.4f;

            highlightTextObject.transform.SetParent(canvasPosition);

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
        inventory.StoreItem(this);
    }
}
