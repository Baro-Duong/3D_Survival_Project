using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Hotbar trước (ưu tiên)
        if (hotBarScreenUI != null)
        {
            foreach (Transform child in hotBarScreenUI.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Slot"))
                    slotList.Add(child.gameObject);
            }
            Debug.Log($"Hotbar slots: {slotList.Count}");
        }
        else
        {
            Debug.LogError("hotBarScreenUI là NULL!");
        }

        int beforeInventory = slotList.Count;
        foreach (Transform child in inventoryScreenUI.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Slot"))
                slotList.Add(child.gameObject);
        }
        Debug.Log($"Inventory slots: {slotList.Count - beforeInventory}, Total: {slotList.Count}");
    }



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



    public void AddToInvetory(string itemName)
    {
        // Thử stack vào slot đã có item cùng loại trước
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

        // Không stack được → tìm slot trống
        whatSlotToEquip = FindNextEmptySlot();

        GameObject prefab = Resources.Load<GameObject>(itemName);
        if (prefab == null)
        {
            Debug.LogError("Không tìm thấy UI prefab cho item: " + itemName);
            return;
        }

        itemToAdd = Instantiate(prefab, whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemToAdd.transform.localPosition = Vector2.zero;
        itemList.Add(itemName);

        ItemSlot newSlot = whatSlotToEquip.GetComponent<ItemSlot>();
        if (newSlot != null) newSlot.RefreshStackDisplay();
    }

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