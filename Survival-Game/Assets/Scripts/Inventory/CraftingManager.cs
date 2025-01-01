using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private GameObject craftingRecipeUIObj;
    [SerializeField] private List<CraftingRecipeData> recipes;

    private Transform craftingListTransform;

    private void Awake()
    {
        craftingListTransform = transform.Find("ScrollableArea").Find("CraftingList");

        foreach (CraftingRecipeData recipe in recipes)
        {
            GameObject recipeEntryObj = Instantiate(craftingRecipeUIObj, craftingListTransform);
            CraftingRecipe craftingRecipe = recipeEntryObj.GetComponent<CraftingRecipe>();
            craftingRecipe.UpdateRecipeUI(recipe);
        }
    }
}
