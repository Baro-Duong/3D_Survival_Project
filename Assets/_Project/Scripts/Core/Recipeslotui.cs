using UnityEngine;
using UnityEngine.UI;
using TMPro;

// One recipe entry inside the Tool Library page; shows the recipe and lets the player choose it
public class RecipeSlotUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image input1Icon;
    public Image input2Icon;
    public Image outputIcon;
    public TMP_Text recipeNameText;
    public Button chooseButton;

    [Header("Ingredient Counts (optional)")]
    public TMP_Text input1CountText;
    public TMP_Text input2CountText;

    private CraftingRecipe currentRecipe;

    // Wires up the Choose button
    private void Start()
    {
        if (chooseButton != null)
            chooseButton.onClick.AddListener(OnChoose);
    }

    // Shows this slot with the given recipe's icons/name and refreshes the Choose button state
    public void Display(CraftingRecipe recipe)
    {
        currentRecipe = recipe;
        gameObject.SetActive(true);

        recipeNameText.text = recipe.recipeName;

        if (recipe.input1Icon != null) input1Icon.sprite = recipe.input1Icon;
        if (recipe.input2Icon != null) input2Icon.sprite = recipe.input2Icon;
        if (recipe.outputIcon != null) outputIcon.sprite = recipe.outputIcon;

        if (input1CountText != null) input1CountText.text = "x" + recipe.input1Count;
        if (input2CountText != null) input2CountText.text = "x" + recipe.input2Count;

        RefreshButtonState();
    }

    // Empties and hides this slot (used when there's no recipe for this page position)
    public void Clear()
    {
        currentRecipe = null;
        gameObject.SetActive(false);
    }

    // Enables the Choose button only if the player has enough of both ingredients
    private void RefreshButtonState()
    {
        if (chooseButton == null || currentRecipe == null) return;

        bool hasInput1 = HasEnoughInInventory(currentRecipe.input1Name, currentRecipe.input1Count);
        bool hasInput2 = HasEnoughInInventory(currentRecipe.input2Name, currentRecipe.input2Count);

        chooseButton.interactable = hasInput1 && hasInput2;
    }

    // Sums the stack counts of every inventory slot holding the given item and checks it meets requiredCount
    private bool HasEnoughInInventory(string itemName, int requiredCount)
    {
        int total = 0;

        foreach (GameObject slot in InventorySystem.Instance.slotList)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            GameObject item = itemSlot != null ? itemSlot.Item : null;
            if (item == null) continue;

            ItemData data = item.GetComponent<ItemData>();
            string childName = data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();
            if (childName != itemName) continue;

            total += data != null ? data.currentStack : 1;
            if (total >= requiredCount) return true;
        }
        return false;
    }

    // Moves the recipe's required ingredient quantities from inventory into the crafting slots and closes the Tool Library
    private void OnChoose()
    {
        if (currentRecipe == null) return;

        bool hasInput1 = HasEnoughInInventory(currentRecipe.input1Name, currentRecipe.input1Count);
        bool hasInput2 = HasEnoughInInventory(currentRecipe.input2Name, currentRecipe.input2Count);

        if (!hasInput1 || !hasInput2)
        {
            string missing = "";
            if (!hasInput1) missing += currentRecipe.input1Name + " ";
            if (!hasInput2) missing += currentRecipe.input2Name;
            Debug.Log("Not enough ingredients! Missing: " + missing.Trim());
            return;
        }

        MoveItemToCraftingSlot(currentRecipe.input1Name, currentRecipe.input1Count, CraftingSystem.Instance.input1Slot);
        MoveItemToCraftingSlot(currentRecipe.input2Name, currentRecipe.input2Count, CraftingSystem.Instance.input2Slot);

        CraftingSystem.Instance.CheckRecipe();
        ToolLibraryUI.Instance.Close();
    }

    // Gathers requiredCount units of itemName from across the inventory (possibly several slots) and places
    // a single stacked item representing that quantity into the given crafting slot
    private void MoveItemToCraftingSlot(string itemName, int requiredCount, CraftingSlot craftingSlot)
    {
        InventorySystem inv = InventorySystem.Instance;

        if (craftingSlot.Item != null)
        {
            craftingSlot.Item.transform.SetParent(null);
            Destroy(craftingSlot.Item);
        }

        int remaining = requiredCount;

        foreach (GameObject slot in inv.slotList)
        {
            if (remaining <= 0) break;

            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot == null) continue;

            GameObject item = itemSlot.Item;
            if (item == null) continue;

            ItemData data = item.GetComponent<ItemData>();
            string childName = data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();
            if (childName != itemName) continue;

            int available = data != null ? data.currentStack : 1;
            int takeFromThisSlot = Mathf.Min(available, remaining);

            if (data != null && data.currentStack > takeFromThisSlot)
            {
                data.currentStack -= takeFromThisSlot;
            }
            else
            {
                // Unparent before Destroy — Destroy is deferred to end of frame, so without this,
                // RefreshStackDisplay() below would still find the item (with its old currentStack)
                // and show a stale count for the rest of this frame
                item.transform.SetParent(null);
                Destroy(item);
            }

            itemSlot.RefreshStackDisplay();

            for (int i = 0; i < takeFromThisSlot; i++)
                inv.itemList.Remove(itemName);

            remaining -= takeFromThisSlot;
        }

        GameObject prefab = Resources.Load<GameObject>(itemName);
        if (prefab == null) { Debug.LogError("Prefab not found: " + itemName); return; }

        GameObject spawned = Instantiate(prefab, craftingSlot.transform.position, craftingSlot.transform.rotation);
        spawned.transform.SetParent(craftingSlot.transform);
        spawned.transform.localPosition = Vector2.zero;

        ItemData spawnedData = spawned.GetComponent<ItemData>();
        if (spawnedData != null) spawnedData.currentStack = requiredCount;

        craftingSlot.RefreshStackDisplay();
    }
}
