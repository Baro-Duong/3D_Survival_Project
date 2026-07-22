using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the hotbar + inventory slots: scanning them, adding items (with stacking), and open/close state
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public GameObject hotBarScreenUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    public bool isFull;

    // Singleton setup
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

    // Resets open/full state and builds the slot list
    void Start()
    {
        isOpen = false;
        isFull = false;
        PopulateSlotList();
    }

    // Collects every "Slot"-tagged child under the hotbar (first) then the inventory screen, in that order
    private void PopulateSlotList()
    {
        if (hotBarScreenUI != null)
        {
            foreach (Transform child in hotBarScreenUI.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Slot"))
                    slotList.Add(child.gameObject);
            }
        }
        else
        {
            Debug.LogError("hotBarScreenUI is NULL!");
        }

        foreach (Transform child in inventoryScreenUI.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Slot"))
                slotList.Add(child.gameObject);
        }
    }

    // Toggles the inventory/crafting screen open and closed with the E key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            if (CraftingSystem.Instance != null) CraftingSystem.Instance.Show();
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if (CraftingSystem.Instance != null) CraftingSystem.Instance.Hide();
            if (ToolLibraryUI.Instance != null) ToolLibraryUI.Instance.Close();
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    // Stacks the item onto a matching existing stack if possible, otherwise spawns it into the next empty slot
    public void AddToInvetory(string itemName)
    {
        foreach (GameObject slot in slotList)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot == null) continue;

            GameObject existingItem = itemSlot.Item;
            if (existingItem == null) continue;

            ItemData existingData = existingItem.GetComponent<ItemData>();
            if (existingData != null
                && existingData.itemName == itemName
                && existingData.currentStack < existingData.maxStack)
            {
                existingData.currentStack++;
                itemList.Add(itemName);
                itemSlot.RefreshStackDisplay();
                return;
            }
        }

        // No matching stack: find an empty slot instead
        whatSlotToEquip = FindNextEmptySlot();

        GameObject prefab = Resources.Load<GameObject>(itemName);
        if (prefab == null)
        {
            Debug.LogError("UI prefab not found for item: " + itemName);
            return;
        }

        itemToAdd = Instantiate(prefab, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemToAdd.transform.localPosition = Vector2.zero;
        itemList.Add(itemName);

        ItemSlot newSlot = whatSlotToEquip.GetComponent<ItemSlot>();
        if (newSlot != null) newSlot.RefreshStackDisplay();
    }

    // Returns the first empty slot GameObject (or a throwaway empty GameObject if none are free)
    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot != null && itemSlot.Item == null)
                return slot;
        }
        return new GameObject();
    }

    // Returns true (and sets isFull) if every slot currently holds an item
    public bool CheckIfFull()
    {
        foreach (GameObject slot in slotList)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot != null && itemSlot.Item == null)
            {
                isFull = false;
                return false;
            }
        }
        isFull = true;
        return true;
    }
}
