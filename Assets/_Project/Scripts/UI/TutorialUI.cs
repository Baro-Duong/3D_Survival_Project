using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Attached to the Tutorial parent GameObject in MenuScene, which must stay active at all times.
// Paginates the tutorial pages shown as an overlay on top of the main menu.
//
// The Play button starts locked every time this scene loads and unlocks once the player has read
// through to the final page. This is deliberately session-only (no PlayerPrefs) — the tutorial must
// be read again on every launch, since MenuScene (and this object with it) is recreated from scratch
// each time the player returns to it.
public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; set; }

    [Header("References")]
    public GameObject tutorialPanel; // the CHILD panel holding the content — never this GameObject

    [Header("Pages - drag in each page GameObject in reading order")]
    public List<GameObject> pages = new List<GameObject>();

    [Header("Page UI")]
    public TMP_Text pageText;
    public Button nextButton;
    public Button backButton;
    public Button closeButton;
    public Button tutorialBtn; // the "open tutorial" button on the main menu

    [Header("Play Gate")]
    public Button playButton;             // stays locked until the tutorial has been read once
    public bool lockPlayUntilRead = true; // untick to disable the gate entirely

    private int currentPage = 0;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Hides the panel, wires up the buttons and applies the Play lock.
    // Deliberately done in Start() rather than Awake(): deactivating a GameObject before its own Start()
    // has run defers that Start() until the object is next reactivated — the bug that once made the Tool
    // Library need opening twice. Unity guarantees every Awake() finishes before any Start() runs, so by
    // this point every page object has already initialised itself.
    private void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
        else
            Debug.LogError("TutorialUI: tutorialPanel is not assigned!");

        nextButton.onClick.AddListener(NextPage);
        backButton.onClick.AddListener(PrevPage);
        closeButton.onClick.AddListener(Close);

        if (tutorialBtn != null)
            tutorialBtn.onClick.AddListener(Open);
        else
            Debug.LogError("TutorialUI: tutorialBtn is not assigned!");

        if (playButton != null) playButton.interactable = !lockPlayUntilRead;
        RefreshDisplay();
    }

    // Opens the overlay at the first page
    public void Open()
    {
        if (tutorialPanel == null) return;

        tutorialPanel.SetActive(true);
        currentPage = 0;
        RefreshDisplay();
    }

    // Closes the overlay and returns to the plain menu
    public void Close()
    {
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
    }

    // Advances one page, if there is one
    private void NextPage()
    {
        if (currentPage < pages.Count - 1)
        {
            currentPage++;
            RefreshDisplay();
        }
    }

    // Goes back one page, if there is one
    private void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            RefreshDisplay();
        }
    }

    // Shows only the current page, updates the counter and the next/back state, and unlocks Play once
    // the final page has been reached
    private void RefreshDisplay()
    {
        int totalPages = Mathf.Max(1, pages.Count);

        for (int i = 0; i < pages.Count; i++)
            if (pages[i] != null) pages[i].SetActive(i == currentPage);

        if (pageText != null) pageText.text = $"Page: {currentPage + 1}/{totalPages}";

        backButton.interactable = currentPage > 0;
        nextButton.interactable = currentPage < pages.Count - 1;

        // Only unlocks while the overlay is actually on screen. Without this check the call in
        // Start() would unlock Play immediately whenever the tutorial has just a single page.
        if (currentPage >= pages.Count - 1 && tutorialPanel != null && tutorialPanel.activeSelf && playButton != null)
            playButton.interactable = true;
    }
}
