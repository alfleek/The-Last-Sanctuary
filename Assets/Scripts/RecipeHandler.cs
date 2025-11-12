using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public List<string> Ingredients;
    public List<string> Needed;
    public List<string> Have;
}

public class RecipeHandler : MonoBehaviour
{
    public static RecipeHandler Instance;

    public Text ObjectTitle;
    public Text Description;
    public Image ObjectImage;
    public Text Ingredent1, Ingredent2, Ingredent3, Ingredent4;
    public Text IngredentNeeded1, IngredentNeeded2, IngredentNeeded3, IngredentNeeded4;
    public Text UserHas1, UserHas2, UserHas3, UserHas4;

    private Dictionary<string, Recipe> recipeBook = new Dictionary<string, Recipe>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Survival recipes
        Recipe Campfire = new Recipe()
        {
            Name = "Campfire",
            Description = "A simple stone campfire used for cooking and warmth.",
            Icon = null, // assign Sprite if available
            Ingredients = new List<string>() { "Stone", "Stick" },
            Needed = new List<string>() { "8", "4" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe Torch = new Recipe()
        {
            Name = "Torch",
            Description = "Provides light in dark areas.",
            Icon = null,
            Ingredients = new List<string>() { "Stick", "Cloth", "Fat" },
            Needed = new List<string>() { "8", "2", "1" },
            Have = new List<string>() { "0", "0", "0" }
        };

        Recipe Flashlight = new Recipe()
        {
            Name = "Flashlight",
            Description = "Provides light in dark areas.",
            Icon = null,
            Ingredients = new List<string>() { "Glass", "Metal", "Battery", "Wire" },
            Needed = new List<string>() { "1", "2", "2", "3" },
            Have = new List<string>() { "0", "0", "0", "0" }
        };

        Recipe CookingPot = new Recipe()
        {
            Name = "Cooking Pot",
            Description = "Help heat and boil food.",
            Icon = null,
            Ingredients = new List<string>() { "Metal" },
            Needed = new List<string>() { "5" },
            Have = new List<string>() { "0" }
        };

        recipeBook.Add(Campfire.Name, Campfire);
        recipeBook.Add(Torch.Name, Torch);
        recipeBook.Add(Flashlight.Name, Flashlight);
        recipeBook.Add(CookingPot.Name, CookingPot);

        // Medical recipes

        Recipe Bandage = new Recipe()
        {
            Name = "Bandage",
            Description = "Help heal and damage",
            Icon = null,
            Ingredients = new List<string>() { "Cloth" },
            Needed = new List<string>() { "5" },
            Have = new List<string>() { "0" }
        };

        Recipe Splint = new Recipe()
        {
            Name = "Splint",
            Description = "Help heal and broken or displaced bone .",
            Icon = null,
            Ingredients = new List<string>() { "Cloth", "Stick" },
            Needed = new List<string>() { "2", "2" },
            Have = new List<string>() { "0", "0" }
        };

        recipeBook.Add(Bandage.Name, Bandage);
        recipeBook.Add(Splint.Name, Splint);

        // Food Recipes
        Recipe CleanWater = new Recipe()
        {
            Name = "Clean Water",
            Description = "Water to deter Thirst.",
            Icon = null,
            Ingredients = new List<string>() { "Water", "CookingPot" },
            Needed = new List<string>() { "1", "1" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe CookedChicken = new Recipe()
        {
            Name = "Cooked Chicken",
            Description = "Chicken to deter Hunger.",
            Icon = null,
            Ingredients = new List<string>() { "Chicken", "CookingPot" },
            Needed = new List<string>() { "1", "1" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe CookedRabbit = new Recipe()
        {
            Name = "Cooked Rabbit",
            Description = "Rabbit to deter Hunger",
            Icon = null,
            Ingredients = new List<string>() { "Rabbit", "CookingPot" },
            Needed = new List<string>() { "1", "1" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe CookedCannedFood = new Recipe()
        {
            Name = "Cooked Canned Food",
            Description = "Canned Foof to Not die",
            Icon = null,
            Ingredients = new List<string>() { "Can", "CookingPot" },
            Needed = new List<string>() { "1", "1" },
            Have = new List<string>() { "0", "0" }
        };

        recipeBook.Add(CleanWater.Name, CleanWater);
        recipeBook.Add(CookedChicken.Name, CookedChicken);
        recipeBook.Add(CookedRabbit.Name, CookedRabbit);
        recipeBook.Add(CookedCannedFood.Name, CookedCannedFood);


        // Weapon Recipes
        Recipe Arrows = new Recipe()
        {
            Name = "Arrows",
            Description = "Water to deter Thirst.",
            Icon = null,
            Ingredients = new List<string>() { "Sticks", "Stone", "Feather" },
            Needed = new List<string>() { "2", "2", "2" },
            Have = new List<string>() { "0", "0", "0" }
        };

        Recipe WoodenBow = new Recipe()
        {
            Name = "Wooden Bow",
            Description = "Chicken to deter Hunger.",
            Icon = null,
            Ingredients = new List<string>() { "Wood", "String" },
            Needed = new List<string>() { "5", "3" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe Bullets = new Recipe()
        {
            Name = "Bullets",
            Description = "",
            Icon = null,
            Ingredients = new List<string>() { "Metal", "Gunpowder" },
            Needed = new List<string>() { "5", "3" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe Gun = new Recipe()
        {
            Name = "Gun",
            Description = "",
            Icon = null,
            Ingredients = new List<string>() { "Metal", "Stone", },
            Needed = new List<string>() { "20", "5" },
            Have = new List<string>() { "0", "0" }
        };
        Recipe StoneKnife = new Recipe()
        {
            Name = "Stone Knife",
            Description = "",
            Icon = null,
            Ingredients = new List<string>() { "Metal", "Sticks" },
            Needed = new List<string>() { "1", "2" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe Hammer = new Recipe()
        {
            Name = "Hammer",
            Description = "",
            Icon = null,
            Ingredients = new List<string>() { "Metal", "Sticks" },
            Needed = new List<string>() { "3", "2" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe StoneAxe = new Recipe()
        {
            Name = "Stone Axe",
            Description = "",
            Icon = null,
            Ingredients = new List<string>() { "Stone", "Sticks" },
            Needed = new List<string>() { "4", "2" },
            Have = new List<string>() { "0", "0" }
        };

        Recipe SpikeTraps = new Recipe()
        {
            Name = "Spike Traps",
            Description = "Canned Foof to Not die",
            Icon = null,
            Ingredients = new List<string>() { "Sticks", "Hammer" },
            Needed = new List<string>() { "5", "1" },
            Have = new List<string>() { "0", "0" }
        };
        recipeBook.Add(Arrows.Name, Arrows);
        recipeBook.Add(WoodenBow.Name, WoodenBow);
        recipeBook.Add(Bullets.Name, Bullets);
        recipeBook.Add(Gun.Name, Gun);
        recipeBook.Add(StoneKnife.Name, StoneKnife);
        recipeBook.Add(Hammer.Name, Hammer);
        recipeBook.Add(StoneAxe.Name, StoneAxe);
        recipeBook.Add(SpikeTraps.Name, SpikeTraps);
    }

    public void OnButtonClicked(string recipeName)

    {
        Debug.Log($" Button clicked for recipe: {recipeName}");

       /* if (recipeBook.ContainsKey(recipeName))
        {

            Recipe recipe = recipeBook[recipeName];
            Debug.Log($" Recipe found: {recipe.Name}");

            // Update UI

            if (ObjectTitle == null || Description == null || ObjectImage == null)
            {
                Debug.LogError("One or more UI elements (ObjectTitle, Description, ObjectImage) are NOT assigned in the Inspector!");
                return;
            }

            ObjectTitle.text = recipe.Name;
            Description.text = recipe.Description;
            ObjectImage.sprite = recipe.Icon;
           

            Debug.Log("UI base info updated (name, description, icon).");

            // Clear old ingredient text
            Ingredent1.text = Ingredent2.text = Ingredent3.text = Ingredent4.text = "";
            IngredentNeeded1.text = IngredentNeeded2.text = IngredentNeeded3.text = IngredentNeeded4.text = "";
            UserHas1.text = UserHas2.text = UserHas3.text = UserHas4.text = "";
            Debug.Log(" Cleared old ingredient texts.");


            // Fill in available ingredients
            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                Debug.Log($"Setting Ingredient");
                switch (i)
                {
                    case 0: Ingredent1.text = recipe.Ingredients[i]; break;
                    case 1: Ingredent2.text = recipe.Ingredients[i]; break;
                    case 2: Ingredent3.text = recipe.Ingredients[i]; break;
                    case 3: Ingredent4.text = recipe.Ingredients[i]; break;
                }
            }

            // Fill in available ingredients
            for (int i = 0; i < recipe.Needed.Count; i++)
            {
                Debug.Log($"Setting Needed");
                switch (i)
                {
                    case 0: IngredentNeeded1.text = recipe.Needed[i]; break;
                    case 1: IngredentNeeded2.text = recipe.Needed[i]; break;
                    case 2: IngredentNeeded3.text = recipe.Needed[i]; break;
                    case 3: IngredentNeeded4.text = recipe.Needed[i]; break;
                }
            }

            // Fill in available ingredients
            for (int i = 0; i < recipe.Have.Count; i++)
            {
                Debug.Log($" Have[{i}] = {recipe.Have[i]}");

                switch (i)
                {
                    case 0: UserHas1.text = recipe.Have[i]; break;
                    case 1: UserHas2.text = recipe.Have[i]; break;
                    case 2: UserHas3.text = recipe.Have[i]; break;
                    case 3: UserHas4.text = recipe.Have[i]; break;
                }
            }

            Debug.Log($" Finished updating UI for recipe: {recipe.Name}");

        }*/
    }
}
