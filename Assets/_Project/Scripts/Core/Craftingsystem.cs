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
    public void Hide() => craftingScreenUI.SetActive(false);

    // Called whenever an item is dropped into a slot; enables the craft button if the 2 inputs match a recipe
    public void CheckRecipe()
    {
        string item1 = input1Slot.ItemName;
        string item2 = input2Slot.ItemName;

        matchedRecipe = null;

        foreach (CraftingRecipe recipe in allRecipes)
        {
            // Input order doesn't matter (item1+item2 or item2+item1 both match)
            bool match = (recipe.input1Name == item1 && recipe.input2Name == item2)
                      || (recipe.input1Name == item2 && recipe.input2Name == item1);

            if (match)
            {
                matchedRecipe = recipe;
                break;
            }
        }

        craftButton.interactable = (matchedRecipe != null);
    }

    // Consumes the 2 input items and spawns the recipe's output item
    private void OnCraftButtonPressed()
    {
        if (matchedRecipe == null) return;

        string item1Name = input1Slot.ItemName;
        string item2Name = input2Slot.ItemName;

        // Remove the 2 input items
        if (input1Slot.Item != null)
        {
            input1Slot.Item.transform.SetParent(null);
            Destroy(input1Slot.Item);
        }
        if (input2Slot.Item != null)
        {
            input2Slot.Item.transform.SetParent(null);
            Destroy(input2Slot.Item);
        }

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

        craftButton.interactable = false;
        matchedRecipe = null;

        // Re-check in case the slots still match another recipe
        CheckRecipe();
    }
}
