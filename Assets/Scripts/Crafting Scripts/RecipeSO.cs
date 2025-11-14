using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]

public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public string description;
    public Sprite icon;

    public List<string> neededIngredients = new List<string>();
    public List<string> NumNeededIngredients = new List<string>();
    public List<string> NumAvalaibleIngredients = new List<string>();
}