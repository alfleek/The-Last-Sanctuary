using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{
    public Button craftitem;
    public bool Craftable = false;

    public RecipeHandler recipeHandler;

    void Start()
    {
        DisableButton();

        if (recipeHandler == null)
            recipeHandler = FindObjectOfType<RecipeHandler>();

    }

    void Update()
    {
        Craftable = CheckCraftable();

        if (Craftable)
            EnableButton();
        else
            DisableButton();
    }

    public void DisableButton() => craftitem.interactable = false;
    public void EnableButton() => craftitem.interactable = true;

    public bool CheckCraftable()
    {
        if (recipeHandler == null || recipeHandler.currentRecipe == null)
            return false;

        RecipeSO recipe = recipeHandler.currentRecipe;

        for (int i = 0; i < recipe.neededIngredients.Count; i++)
        {
            string ingredient = recipe.neededIngredients[i];
            int needed = recipe.NumNeededIngredients[i];

            if (InventorySystem.Instance.CountItem(ingredient) < needed)
                return false;
        }

        return true;
    }

    public void Craft()
    {
        if (recipeHandler == null || recipeHandler.currentRecipe == null) return;

        RecipeSO recipe = recipeHandler.currentRecipe;
        Debug.Log("1. Crafted Item is :" + recipe.craftedItemName);
        // Remove ingredients
        for (int i = 0; i < recipe.neededIngredients.Count; i++)
        {
            string ingredient = recipe.neededIngredients[i];
            int needed = recipe.NumNeededIngredients[i];
            InventorySystem.Instance.RemoveItem(ingredient, needed);
        }

        // Add crafted item
        Debug.Log("2 .Crafted Item is :"+recipe.craftedItemName);
        InventorySystem.Instance.addToInventory(recipe.craftedItemName);

    }

    public void DebugHelp()
    {
        Debug.Log("Button pressed");
    }
}
