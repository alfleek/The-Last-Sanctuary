using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeHandler : MonoBehaviour
{
    [Header("Recipe Database")]
    public RecipeDatabase survivalRecipes;
    public RecipeDatabase medicalRecipes;
    public RecipeDatabase foodRecipes;
    public RecipeDatabase weaponRecipes;

    [Header("UI Elements")]
    public Text ObjectTitle;
    public Text Description;
    public Image ObjectImage;
    public List<Text> ingredientTexts;   // Use a list instead of separate fields
    public List<Text> neededTexts;
    public List<Text> haveTexts;

    // Call this when a button is clicked
    public void OnButtonClicked(string recipeName)
    {
        Debug.Log("Button IS being clicked");

        RecipeSO recipe = FindRecipeByName(recipeName);
        if (recipe == null)
        {
            Debug.LogWarning("Recipe not found: " + recipeName);
            return;
        }

        // Update UI
        ObjectTitle.text = recipe.recipeName;
        Description.text = recipe.description;
        ObjectImage.sprite = recipe.icon;

        for (int i = 0; i < ingredientTexts.Count; i++)
            ingredientTexts[i].text = i < recipe.neededIngredients.Count ? recipe.neededIngredients[i] : "";

        for (int i = 0; i < neededTexts.Count; i++)
            neededTexts[i].text = i < recipe.NumNeededIngredients.Count ? recipe.NumNeededIngredients[i] : "";

        for (int i = 0; i < haveTexts.Count; i++)
            haveTexts[i].text = i < recipe.NumAvalaibleIngredients.Count ? recipe.NumAvalaibleIngredients[i] : "";
    }

    // Search through all databases
    private RecipeSO FindRecipeByName(string name)
    {
        foreach (var db in new RecipeDatabase[] { survivalRecipes, medicalRecipes, foodRecipes, weaponRecipes })
        {
            RecipeSO recipe = db.recipes.Find(r => r.recipeName == name);
            if (recipe != null) return recipe;
        }
        return null;
    }
}
