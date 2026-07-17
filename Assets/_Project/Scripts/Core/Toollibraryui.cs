using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Gắn vào GameObject ToolLibrary
public class ToolLibraryUI : MonoBehaviour
{
    public static ToolLibraryUI Instance { get; set; }

    [Header("References")]
    public CraftingSystem craftingSystem;
    public GameObject toolLibraryUI;

    [Header("Slot References - kéo 4 Formular slot vào theo thứ tự")]
    public List<RecipeSlotUI> recipeSlots = new List<RecipeSlotUI>();

    [Header("Page UI")]
    public TMP_Text pageText;
    public Button nextButton;
    public Button backButton;
    public Button closeButton;
    public Button toolsLibraryBtn;  // nút ToolsLibraryBTN trong CraftingScreen

    private List<CraftingRecipe> recipes => craftingSystem.allRecipes;
    private int currentPage = 0;
    private int recipesPerPage = 4;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        if (toolLibraryUI != null)
            toolLibraryUI.SetActive(false);

        // Ẩn hết tất cả formular từ đầu
        foreach (var slot in recipeSlots)
            if (slot != null) slot.gameObject.SetActive(false);
    }

    private void Start()
    {
        Debug.Log("ToolLibraryUI Start chạy");
        Debug.Log("toolsLibraryBtn: " + toolsLibraryBtn);

        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(Close);

        if (toolsLibraryBtn != null)
        {
            toolsLibraryBtn.onClick.AddListener(Open);
            Debug.Log("Đã gán onClick cho toolsLibraryBtn");
        }
        else
        {
            Debug.LogError("toolsLibraryBtn là NULL!");
        }

        RefreshDisplay();
    }

    public void Open()
    {
        Debug.Log("Open() được gọi");
        toolLibraryUI.SetActive(true);
        currentPage = 0;
        RefreshDisplay();
    }

    public void Close()
    {
        toolLibraryUI.SetActive(false);
    }

    private void NextPage()
    {
        int totalPages = Mathf.CeilToInt((float)recipes.Count / recipesPerPage);
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            RefreshDisplay();
        }
    }

    private void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            RefreshDisplay();
        }
    }

    private void RefreshDisplay()
    {
        int totalPages = Mathf.CeilToInt((float)recipes.Count / recipesPerPage);
        if (totalPages == 0) totalPages = 1;

        pageText.text = $"Page: {currentPage + 1}/{totalPages}";
        backButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < totalPages - 1;

        for (int i = 0; i < recipeSlots.Count; i++)
        {
            int recipeIndex = currentPage * recipesPerPage + i;
            if (recipeIndex < recipes.Count)
                recipeSlots[i].Display(recipes[recipeIndex]);
            else
                recipeSlots[i].Clear();
        }
    }
}