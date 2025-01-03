using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MerchantManager : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform canvasPosition;
    [SerializeField] private GameObject highlightText;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float maxPlayerDistance = 30f;

    private bool isHighlighted = false;
    private GameObject highlightTextObject;
    private TMP_Text merchantDistanceText;

    public float distanceFromPlayer { get; private set; }

    public static MerchantManager Instance { get; private set; }

    private MerchantAI merchantAI;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        merchantAI = GetComponent<MerchantAI>();
    }

    void Start()
    {
        merchantDistanceText = HUDManager.Instance.gameInfoPanel.transform.Find("MerchantDistance").GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        distanceFromPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (distanceFromPlayer > maxPlayerDistance)
        {
            HUDManager.Instance.inactiveCraftingPanel.SetActive(true);
        }
        else
        {
            HUDManager.Instance.inactiveCraftingPanel.SetActive(false);
        }
        merchantDistanceText.text = "Merchant: " + (int)distanceFromPlayer + "m";
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
            highlightTextComponent.text = "Speak (E)";
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
        merchantAI.SpeakToMerchant();
    }
}
