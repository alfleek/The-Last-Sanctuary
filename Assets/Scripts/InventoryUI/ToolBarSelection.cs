using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarSelection : MonoBehaviour
{

    public List<GameObject> slotList = new List<GameObject>();
    public int activeSlot = 0;
    private int maxSlots;
    public InputManager input;
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Slots"))
            {
                slotList.Add(child.gameObject);
            }
        }
        maxSlots = slotList.Count;
        ActivateSlot(activeSlot);
    }

    //Takes Index - Starts at 0
    public void ActivateSlot(int slot)
    {
        GameObject prevSlot = slotList[activeSlot];
        activeSlot = slot;
        GameObject nextSlot = slotList[activeSlot];
        prevSlot.GetComponent<Image>().color = new Color32(130, 130, 130, 123);
        nextSlot.GetComponent<Image>().color = new Color32(255, 255, 255, 123);

        GameObject prevItemCheck = prevSlot.GetComponent<ToolbarSlot>().Item;
        GameObject nextItemCheck = nextSlot.GetComponent<ToolbarSlot>().Item;

        EquippableItem prevItem = null;
        EquippableItem nextItem = null;
        
        if (prevItemCheck != null) prevItem = prevItemCheck.GetComponent<EquippableItem>();
        if (nextItemCheck != null) nextItem = nextItemCheck.GetComponent<EquippableItem>();

        if (prevItem != null && prevItem.EquipObject != null)
        {
            input.UnequipWeapon();
            prevItem.EquipObject.SetActive(false);
        }
        if (nextItem != null && nextItem.EquipPrefab != null)
        {
            if(nextItem.EquipObject == null)
            {
                GameObject parent = input.gameObject.transform.Find("Main Camera").gameObject;
                nextItem.EquipObject = Instantiate(nextItem.EquipPrefab, parent.transform);
                nextItem.EquipObject.SetActive(true);
            }
            else
            {
                nextItem.EquipObject.SetActive(true);
            }
            

            input.EquipWeapon(nextItem.EquipObject.GetComponent<Weapon>());
        }
    }

    //Takes Count - Starts at 1
    public void SelectSlot(int slot)
    {
        if(slot < 1 || slot > maxSlots) 
            return;
        if(slot - 1 == activeSlot)
            return;
        ActivateSlot(slot - 1);
    }

    public void NextSlot(float right)
    {   
        if(right == 0)
            return;
        int nextSlot = activeSlot + (right > 0 ? 1 : -1);
        if(nextSlot < 0)
            nextSlot = maxSlots - 1;
        if(nextSlot >= maxSlots)
            nextSlot = 0;
        ActivateSlot(nextSlot);
    }

    public GameObject GetActiveSlot()
    {
        return slotList[activeSlot].gameObject;
    }
}
