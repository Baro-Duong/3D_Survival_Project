using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Matches the 2 crafting input slots against the recipe list and crafts the output item
public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; set; }

    [Header("UI References")]
    public GameObject craftingScreenUI;
    public CraftingSlot input1Slot;
    public CraftingSlot input2Slot;
    public CraftingSlot outputSlot;
    public Button craftButton;

    [Header("Recipes")]
    public List<CraftingRecipe> allRecipes = new List<CraftingRecipe>();

    private CraftingRecipe matchedRecipe;
    private bool inputsSwapped; // true if input1Slot actually holds recipe.input2Name (and vice versa)

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Hides the crafting screen and wires up the craft button
    private void Start()
    {
        craftingScreenUI.SetActive(false);
        craftButton.interactable = false;
        craftButton.onClick.AddListener(OnCraftButtonPressed);
    }

    public void Show() => craftingScreenUI.SetActive(true);

    // Hides the crafting screen and returns any leftover items in the 3 slots back to inventory,
    // so nothing gets stranded/lost when the player closes the inventory mid-craft
    public void Hide()
    {
        craftingScreenUI.SetActive(false);

        ReturnSlotToInventory(input1Slot);
        ReturnSlotToInventory(input2Slot);
        ReturnSlotToInventory(outputSlot);

        matchedRecipe = null;
        craftButton.interactable = false;
    }

    // Moves whatever item is in the given crafting slot back into the next empty inventory/hotbar slot(s)
    private void ReturnSlotToInventory(CraftingSlot slot)
    {
        if (slot.Item == null) return;

        ItemData data = slot.Item.GetComponent<ItemData>();
        string itemName = data != null ? data.itemName : slot.Item.name.Replace("(Clone)", "").Trim();
        int count = data != null ? data.currentStack : 1;

        slot.Item.transform.SetParent(null);
        Destroy(slot.Item);
        slot.RefreshStackDisplay();

        for (int i = 0; i < count; i++)
            InventorySystem.Instance.AddToInvetory(itemName);
    }

    // Called whenever an item is dropped into a slot; enables the craft button if the 2 inputs match a
    // recipe both by item name AND by having enough of each in stock
    public void CheckRecipe()
    {
        string item1 = input1Slot.ItemName;
        string item2 = input2Slot.ItemName;

        matchedRecipe = null;
        inputsSwapped = false;
        int bestSpecificity = -1; // total ingredient count of the current best match — prefers the more demanding recipe when several share the same 2 item names

        foreach (CraftingRecipe recipe in allRecipes)
        {
            bool straightMatch = recipe.input1Name == item1 && recipe.input2Name == item2
                && HasEnough(input1Slot, recipe.input1Count) && HasEnough(input2Slot, recipe.input2Count);

            // Input order doesn't matter (item1+item2 or item2+item1 both match)
            bool swappedMatch = !straightMatch && recipe.input1Name == item2 && recipe.input2Name == item1
                && HasEnough(input1Slot, recipe.input2Count) && HasEnough(input2Slot, recipe.input1Count);

            if (!straightMatch && !swappedMatch) continue;

            int specificity = recipe.input1Count + recipe.input2Count;
            if (specificity > bestSpecificity)
            {
                bestSpecificity = specificity;
                matchedRecipe = recipe;
                inputsSwapped = swappedMatch;
            }
        }

        craftButton.interactable = (matchedRecipe != null);
    }

    // Returns true if the slot's item stack has at least `required` units
    private bool HasEnough(CraftingSlot slot, int required)
    {
        if (slot.Item == null) return false;
        ItemData data = slot.Item.GetComponent<ItemData>();
        return data != null && data.currentStack >= required;
    }

    // Consumes the required quantity from each input and spawns the recipe's output item
    private void OnCraftButtonPressed()
    {
        if (matchedRecipe == null) return;

        int input1Required = inputsSwapped ? matchedRecipe.input2Count : matchedRecipe.input1Count;
        int input2Required = inputsSwapped ? matchedRecipe.input1Count : matchedRecipe.input2Count;

        ConsumeFromSlot(input1Slot, input1Required);
        ConsumeFromSlot(input2Slot, input2Required);

        // Remove any leftover item in the output slot
        if (outputSlot.Item != null)
        {
            outputSlot.Item.transform.SetParent(null);
            Destroy(outputSlot.Item);
        }

        // Spawn the crafted item into the output slot
        GameObject prefab = Resources.Load<GameObject>(matchedRecipe.outputName);
        if (prefab == null)
        {
            Debug.LogError("Prefab not found: " + matchedRecipe.outputName);
            return;
        }

        GameObject outputItem = Instantiate(prefab, outputSlot.transform.position, outputSlot.transform.rotation);
        outputItem.transform.SetParent(outputSlot.transform);
        outputItem.transform.localPosition = Vector2.zero;
        outputSlot.RefreshStackDisplay();

        craftButton.interactable = false;
        matchedRecipe = null;

        // Re-check in case the slots still match another recipe
        CheckRecipe();
    }

    // Removes `amount` from the slot's item stack, destroying the item only once its stack hits 0
    private void ConsumeFromSlot(CraftingSlot slot, int amount)
    {
        if (slot.Item == null) return;

        ItemData data = slot.Item.GetComponent<ItemData>();
        string itemName = data != null ? data.itemName : slot.Item.name.Replace("(Clone)", "").Trim();

        for (int i = 0; i < amount; i++)
            InventorySystem.Instance.itemList.Remove(itemName);

        if (data != null && data.currentStack > amount)
        {
            data.currentStack -= amount;
        }
        else
        {
            slot.Item.transform.SetParent(null);
            Destroy(slot.Item);
        }

        slot.RefreshStackDisplay();
    }
}
