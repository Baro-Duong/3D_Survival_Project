using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeSlotUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image input1Icon;
    public Image input2Icon;
    public Image outputIcon;
    public TMP_Text recipeNameText;
    public Button chooseButton;

    private CraftingRecipe currentRecipe;

    private void Start()
    {
        if (chooseButton != null)
            chooseButton.onClick.AddListener(OnChoose);
    }

    public void Display(CraftingRecipe recipe)
    {
        currentRecipe = recipe;
        gameObject.SetActive(true);

        recipeNameText.text = recipe.recipeName;

        if (recipe.input1Icon != null) input1Icon.sprite = recipe.input1Icon;
        if (recipe.input2Icon != null) input2Icon.sprite = recipe.input2Icon;
        if (recipe.outputIcon != null) outputIcon.sprite = recipe.outputIcon;

        // Đổi tên nút thành "Choose"

        RefreshButtonState();
    }

    public void Clear()
    {
        currentRecipe = null;
        gameObject.SetActive(false);
    }

    private void RefreshButtonState()
    {
        if (chooseButton == null || currentRecipe == null) return;

        bool hasInput1 = HasItemInInventory(currentRecipe.input1Name);
        bool hasInput2 = HasItemInInventory(currentRecipe.input2Name);

        chooseButton.interactable = hasInput1 && hasInput2;
    }

    private bool HasItemInInventory(string itemName)
    {
        // Check itemList trước
        if (InventorySystem.Instance.itemList.Contains(itemName)) return true;

        // Check slot UI (trường hợp item được đặt thủ công trong Editor)
        foreach (GameObject slot in InventorySystem.Instance.slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string childName = slot.transform.GetChild(0).gameObject.name
                    .Replace("(Clone)", "").Trim();
                if (childName == itemName) return true;
            }
        }
        return false;
    }

    private void OnChoose()
    {
        if (currentRecipe == null) return;

        bool hasInput1 = HasItemInInventory(currentRecipe.input1Name);
        bool hasInput2 = HasItemInInventory(currentRecipe.input2Name);

        if (!hasInput1 || !hasInput2)
        {
            string missing = "";
            if (!hasInput1) missing += currentRecipe.input1Name + " ";
            if (!hasInput2) missing += currentRecipe.input2Name;
            Debug.Log("Không đủ nguyên liệu! Cần: " + missing.Trim());
            return;
        }

        // Di chuyển item từ inventory vào crafting slot (không spawn mới)
        MoveItemToCraftingSlot(currentRecipe.input1Name, CraftingSystem.Instance.input1Slot);
        MoveItemToCraftingSlot(currentRecipe.input2Name, CraftingSystem.Instance.input2Slot);

        CraftingSystem.Instance.CheckRecipe();
        ToolLibraryUI.Instance.Close();
    }

    private void MoveItemToCraftingSlot(string itemName, CraftingSlot craftingSlot)
    {
        InventorySystem inv = InventorySystem.Instance;

        foreach (GameObject slot in inv.slotList)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot == null) continue;

            GameObject item = itemSlot.Item;
            if (item == null) continue;

            ItemData data = item.GetComponent<ItemData>();
            string childName = data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();

            if (childName == itemName)
            {
                // Xóa slot cũ trong crafting nếu có
                if (craftingSlot.Item != null)
                {
                    craftingSlot.Item.transform.SetParent(null);
                    Destroy(craftingSlot.Item);
                }

                if (data != null && data.currentStack > 1)
                {
                    // Có nhiều hơn 1 → giảm stack, spawn mới vào crafting slot
                    data.currentStack--;
                    itemSlot.RefreshStackDisplay();
                    inv.itemList.Remove(itemName);

                    GameObject prefab = Resources.Load<GameObject>(itemName);
                    GameObject spawned = Instantiate(prefab, craftingSlot.transform.position, craftingSlot.transform.rotation);
                    spawned.transform.SetParent(craftingSlot.transform);
                    spawned.transform.localPosition = Vector2.zero;
                }
                else
                {
                    // Stack = 1 → di chuyển thẳng item vào crafting slot
                    item.transform.SetParent(craftingSlot.transform);
                    item.transform.localPosition = Vector2.zero;
                    itemSlot.RefreshStackDisplay();
                    inv.itemList.Remove(itemName);
                }
                return;
            }
        }
    }
}