using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Attached to the ToolLibrary GameObject; paginates all recipes, 4 per page
public class ToolLibraryUI : MonoBehaviour
{
    public static ToolLibraryUI Instance { get; set; }

    [Header("References")]
    public CraftingSystem craftingSystem;
    public GameObject toolLibraryUI;

    [Header("Slot References - drag in the 4 recipe slots in order")]
    public List<RecipeSlotUI> recipeSlots = new List<RecipeSlotUI>();

    [Header("Page UI")]
    public TMP_Text pageText;
    public Button nextButton;
    public Button backButton;
    public Button closeButton;
    public Button toolsLibraryBtn;  // the "open library" button on the crafting screen

    private List<CraftingRecipe> recipes => craftingSystem.allRecipes;
    private int currentPage = 0;
    private int recipesPerPage = 4;

    // Singleton setup; hides the panel and all recipe slots at start
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        if (toolLibraryUI != null)
            toolLibraryUI.SetActive(false);

        foreach (var slot in recipeSlots)
            if (slot != null) slot.gameObject.SetActive(false);
    }

    // Wires up all the buttons and shows the first page
    private void Start()
    {
        Debug.Log("ToolLibraryUI Start running");
        Debug.Log("toolsLibraryBtn: " + toolsLibraryBtn);

        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(Close);

        if (toolsLibraryBtn != null)
        {
            toolsLibraryBtn.onClick.AddListener(Open);
            Debug.Log("onClick assigned to toolsLibraryBtn");
        }
        else
        {
            Debug.LogError("toolsLibraryBtn is NULL!");
        }

        RefreshDisplay();
    }

    // Opens the panel back at page 1
    public void Open()
    {
        Debug.Log("Open() called");
        toolLibraryUI.SetActive(true);
        currentPage = 0;
        RefreshDisplay();
    }

    // Closes the panel
    public void Close()
    {
        toolLibraryUI.SetActive(false);
    }

    // Advances to the next page, if any
    private void NextPage()
    {
        int totalPages = Mathf.CeilToInt((float)recipes.Count / recipesPerPage);
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            RefreshDisplay();
        }
    }

    // Goes back to the previous page, if any
    private void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            RefreshDisplay();
        }
    }

    // Updates the page label, next/back button state, and fills the 4 recipe slots for the current page
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
