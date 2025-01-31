using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipe : MonoBehaviour
{
    [SerializeField] private GameObject resourceUIObj;
    [SerializeField] private CraftingManager craftingManager;

    private Transform ingredientsListTransform;
    private Transform resultTransform;
    private Button craftButton;
    private Image background;

    private PlayerInventory inventory;
    private CraftingRecipeData currentRecipe;
    private bool isCraftable = true;


    private void Awake()
    {
        ingredientsListTransform = transform.Find("IngredientsScrollArea").Find("IngredientsList");
        resultTransform = transform.Find("Result");
        craftButton = transform.Find("CraftButton").GetComponent<Button>();
        background = GetComponent<Image>();

        craftingManager = FindAnyObjectByType<CraftingManager>();

        inventory = PlayerInventory.Instance;
    }

    private void Update()
    {
        if (currentRecipe == null) return;
        foreach (InventorySlot ingredient in currentRecipe.ingredients)
        {
            if (currentRecipe.name == "ArmourCraftingRecipe") //check if armour reached max level
            {
                int currentArmourLevel = PlayerArmour.instance.GetArmourLevel();
                if (currentArmourLevel >= PlayerArmour.instance.GetMaxArmourLevel())
                {
                    isCraftable = false;
                    break;
                }
            }

            // Not craftable when player does not have all quantities of ingredients
            if (inventory.HasItem(ingredient.Item.itemName) < ingredient.Quantity)
            {
                isCraftable = false;
                break;
            }
            else
            {
                isCraftable = true;
            }
        }
        craftButton.interactable = isCraftable;
        background.color = isCraftable ? Color.green : Color.grey;

    }

    public void UpdateRecipeUI(CraftingRecipeData recipe)
    {
        // Update UI for ingredients
        foreach (InventorySlot ingredient in recipe.ingredients)
        {
            GameObject ingredientObj = Instantiate(resourceUIObj, ingredientsListTransform);
            ResourceUI ingredientUI = ingredientObj.GetComponent<ResourceUI>();
            ingredientUI.UpdateUI(ingredient);
        }

        // Update UI for crafted item
        GameObject resultObj = Instantiate(resourceUIObj, resultTransform);
        ResourceUI resultUI = resultObj.GetComponent<ResourceUI>();
        resultUI.UpdateUI(recipe.craftedItem);

        currentRecipe = recipe;
    }


    public void Craft()
    {
        if (!isCraftable) return;

        // Consume ingredients
        foreach (InventorySlot ingredient in currentRecipe.ingredients)
        {
            inventory.ConsumeItem(ingredient.Item.itemName, ingredient.Quantity);
        }

        if (currentRecipe.name == "ArmourCraftingRecipe") //Don't instantiate object
        {
            PlayerArmour.instance.UpgradeArmourLevel();
            return;
        }
        for (int i = 0; i < currentRecipe.craftedItem.Quantity; i++)
        {
            GameObject craftedResourceObj = Instantiate(currentRecipe.craftedItem.Item.gameObject, null);
            Resource craftedResource = craftedResourceObj.GetComponent<Resource>();

            int index = inventory.StoreItem(craftedResource);

            // Inventory full
            if (index == -1)
            {
                craftedResourceObj.transform.position = inventory.transform.position;
            }
        }
    }
}
