using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingScript : MonoBehaviour
{

    [Header("Recipe Book Panel Elements")]
    public GameObject SurvivalTab;
    public GameObject FoodTab;
    public GameObject WeaponsTab;
    public GameObject MedicalTab;
    public Text CurrentTab;

    private string WhatTabOpen = "Survival";

    // Start is called before the first frame update
    void Start()
    {
        if (SurvivalTab != null) SurvivalTab.SetActive(true);
        if (FoodTab != null) FoodTab.SetActive(false);
        if (WeaponsTab != null) WeaponsTab.SetActive(false);
        if (MedicalTab != null) MedicalTab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTab != null) CurrentTab.text = WhatTabOpen;


    }

    public void OpenSurvivalRecipes()
    {
        WhatTabOpen = "Survival";
        if (SurvivalTab != null) SurvivalTab.SetActive(true);
        if (FoodTab != null) FoodTab.SetActive(false);
        if (WeaponsTab != null) WeaponsTab.SetActive(false);
        if (MedicalTab != null) MedicalTab.SetActive(false);
    }

    public void OpenFoodRecipes()
    {
        WhatTabOpen = "Food";
        if (SurvivalTab != null) SurvivalTab.SetActive(false);
        if (FoodTab != null) FoodTab.SetActive(true);
        if (WeaponsTab != null) WeaponsTab.SetActive(false);
        if (MedicalTab != null) MedicalTab.SetActive(false);
    }

    public void OpenWeaponsRecipes()
    {
        WhatTabOpen = "Weapons";
        if (SurvivalTab != null) SurvivalTab.SetActive(false);
        if (FoodTab != null) FoodTab.SetActive(false);
        if (WeaponsTab != null) WeaponsTab.SetActive(true);
        if (MedicalTab != null) MedicalTab.SetActive(false);
    }

    public void OpenMedicalRecipes()
    {
        WhatTabOpen = "Medical";
        if (SurvivalTab != null) SurvivalTab.SetActive(false);
        if (FoodTab != null) FoodTab.SetActive(false);
        if (WeaponsTab != null) WeaponsTab.SetActive(false);
        if (MedicalTab != null) MedicalTab.SetActive(true);
    }


}

