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

        RefreshButtonState();
    }

    // Empties and hides this slot (used when there's no recipe for this page position)
    public void Clear()
    {
        currentRecipe = null;
        gameObject.SetActive(false);
    }

    // Enables the Choose button only if the player has both ingredients
    private void RefreshButtonState()
    {
        if (chooseButton == null || currentRecipe == null) return;

        bool hasInput1 = HasItemInInventory(currentRecipe.input1Name);
        bool hasInput2 = HasItemInInventory(currentRecipe.input2Name);

        chooseButton.interactable = hasInput1 && hasInput2;
    }

    // Checks whether the player currently holds the given item, in the item list or in a slot
    private bool HasItemInInventory(string itemName)
    {
        if (InventorySystem.Instance.itemList.Contains(itemName)) return true;

        // Also scan slot children directly (covers items placed manually in the Editor)
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

    // Moves the recipe's 2 ingredients from inventory into the crafting slots and closes the Tool Library
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
            Debug.Log("Not enough ingredients! Missing: " + missing.Trim());
            return;
        }

        // Move the real items (don't spawn new ones) into the crafting slots
        MoveItemToCraftingSlot(currentRecipe.input1Name, CraftingSystem.Instance.input1Slot);
        MoveItemToCraftingSlot(currentRecipe.input2Name, CraftingSystem.Instance.input2Slot);

        CraftingSystem.Instance.CheckRecipe();
        ToolLibraryUI.Instance.Close();
    }

    // Finds the named item in inventory and relocates it into the given crafting slot (splitting a stack if needed)
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
                // Clear out whatever was already in the crafting slot
                if (craftingSlot.Item != null)
                {
                    craftingSlot.Item.transform.SetParent(null);
                    Destroy(craftingSlot.Item);
                }

                if (data != null && data.currentStack > 1)
                {
                    // More than 1 in the stack: decrement it and spawn a single new item into the crafting slot
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
                    // Exactly 1: move the actual item into the crafting slot
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
