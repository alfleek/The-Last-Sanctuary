using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public string description;
    public Sprite icon;

    public string craftedItemName;

    public List<string> neededIngredients = new List<string>();
    public List<int> NumNeededIngredients = new List<int>();
}
