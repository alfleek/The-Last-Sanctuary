using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarSelection : MonoBehaviour
{

    public List<GameObject> slotList = new List<GameObject>();
    public int activeSlot = 0;
    private int maxSlots = 4;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Slots"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    private void ActivateSlot()
    {
        
    }

    public void SelectSlot(int slot)
    {
        if(slot < 1 || slot > 5) 
            return;
        
    }

    public void NextSlot(bool right)
    {
        
    }
}
