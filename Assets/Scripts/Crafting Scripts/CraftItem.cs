using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{
    public Button craftitem;
    public bool Craftable = false; 

    // Start is called before the first frame update
    void Start()
    {
        DisableButton();
    }

    // Update is called once per frame
    void Update()
    {
        Craftable = CheckCraftable(); 
        //Check if Craftable
        if (Craftable == true)
        {
            EnableButton();
        }
    }

    public void DisableButton()
    {
        craftitem.interactable = false;
    }

    public void EnableButton()
    {
        craftitem.interactable = true;
    }

    public bool CheckCraftable()
    {
        return false; 
    }
}
