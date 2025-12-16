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
    public List<Text> ingredientTexts;
    public List<Text> neededTexts;
    public List<Text> haveTexts;

    public RecipeSO currentRecipe; // currently selected recipe

    public void OnButtonClicked(string recipeName)
    {
        RecipeSO recipe = FindRecipeByName(recipeName);
        if (recipe == null)
        {
            Debug.LogWarning("Recipe not found: " + recipeName);
            return;
        }

        currentRecipe = recipe;

        // Update UI
        ObjectTitle.text = recipe.recipeName;
        Description.text = recipe.description;
        ObjectImage.sprite = recipe.icon;

        for (int i = 0; i < ingredientTexts.Count; i++)
        {
            if (i < recipe.neededIngredients.Count)
            {
                string ingredient = recipe.neededIngredients[i];
                int needed = recipe.NumNeededIngredients[i];
                int have = InventorySystem.Instance.CountItem(ingredient);

                ingredientTexts[i].text = ingredient;
                neededTexts[i].text = needed.ToString();
                haveTexts[i].text = have.ToString();
            }
            else
            {
                ingredientTexts[i].text = "";
                neededTexts[i].text = "";
                haveTexts[i].text = "";
            }
        }
    }

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
