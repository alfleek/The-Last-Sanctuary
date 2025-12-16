using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public bool isOpen;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlot;

    public bool isFull;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slots"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     inventoryScreenUI.SetActive(true);
        //     isOpen = true;
        // }
        // else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        // {
        //     inventoryScreenUI.SetActive(false);
        //     isOpen = false;
        // }
    }

    public void addToInventory(string itemName)
    {
        GameObject prefab = Resources.Load<GameObject>(itemName);

        if (prefab == null)
        {
            Debug.LogError($"Cannot add null item to inventory! Missing prefab in Resources: {itemName}");
            return;
        }

        GameObject slot = FindNextSlot();
        if (slot == null)
        {
            Debug.LogError("No empty inventory slot found!");
            return;
        }

        GameObject item = Instantiate(prefab, slot.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        itemList.Add(itemName);
    }


    public void RemoveItem(string itemName, int amount)
    {
        int removed = 0;

        foreach (GameObject slot in slotList)
        {
            // Skip empty slots
            if (slot.transform.childCount == 0) continue;

            // Check if the item in slot matches
            GameObject child = slot.transform.GetChild(0).gameObject;
            if (child.name.Replace("(Clone)", "") == itemName)
            {
                Destroy(child);
                removed++;
                itemList.Remove(itemName);
            }

            if (removed >= amount)
                break;
        }

        if (removed < amount)
            Debug.LogWarning($"Tried to remove {amount} of {itemName} but only removed {removed}.");
    }


    public int CountItem(string itemName)
    {
        int count = 0;
        foreach (var item in itemList)
        {
            if (item == itemName) count++;
        }
        return count;
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }

        return counter >= slotList.Count;
    }

    private GameObject FindNextSlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }

        return null;
    }
}
