using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        craftingScreenUI.SetActive(false);
        craftButton.interactable = false;
        craftButton.onClick.AddListener(OnCraftButtonPressed);
    }

    public void Show() => craftingScreenUI.SetActive(true);
    public void Hide() => craftingScreenUI.SetActive(false);

    // Gọi mỗi khi item được drop vào slot
    public void CheckRecipe()
    {
        string item1 = input1Slot.ItemName;
        string item2 = input2Slot.ItemName;

        matchedRecipe = null;

        foreach (CraftingRecipe recipe in allRecipes)
        {
            // Input không cần đúng thứ tự (item1+item2 hoặc item2+item1 đều được)
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

    private void OnCraftButtonPressed()
    {
        if (matchedRecipe == null) return;

        // Lấy tên item trước khi xóa
        string item1Name = input1Slot.ItemName;
        string item2Name = input2Slot.ItemName;

        // Tách item ra khỏi slot trước khi destroy
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

        // Xóa item cũ ở output nếu có
        if (outputSlot.Item != null)
        {
            outputSlot.Item.transform.SetParent(null);
            Destroy(outputSlot.Item);
        }

        // Spawn output item vào outputSlot
        GameObject prefab = Resources.Load<GameObject>(matchedRecipe.outputName);
        if (prefab == null)
        {
            Debug.LogError("Không tìm thấy prefab: " + matchedRecipe.outputName);
            return;
        }

        GameObject outputItem = Instantiate(prefab, outputSlot.transform.position, outputSlot.transform.rotation);
        outputItem.transform.SetParent(outputSlot.transform);
        outputItem.transform.localPosition = Vector2.zero;

        craftButton.interactable = false;
        matchedRecipe = null;

        // Check lại recipe sau khi craft
        CheckRecipe();
    }
}