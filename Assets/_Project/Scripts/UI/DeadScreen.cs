using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Shows the Game Over panel when the player dies: fades in a red overlay, freezes player control,
// and wires up the Restart/Main Menu buttons
public class DeadScreen : MonoBehaviour
{
    public static DeadScreen Instance { get; set; }

    [Header("UI")]
    public GameObject deadScreenUI;
    public Image redBackground;

    [Header("Timer")]
    public GameObject timerHUD;        // the "Timer" box shown during gameplay, hidden on death
    public TMP_Text liveTimerText;     // ticks up every frame while playing
    public TMP_Text survivalTimeText;  // "Your Time" text inside deadScreenUI, set once on death

    [Header("Fade")]
    public float fadeDuration = 2f;
    public float targetAlpha = 0.85f;

    [Header("Disable on death")]
    public PlayerMovement playerMovement;
    public MouseMovement mouseMovement;
    public PlayerAttack playerAttack;
    public HotbarSelection hotbarSelection;
    public PotInteraction potInteraction;

    [Header("Scenes")]
    public string mainMenuSceneName = "MenuScene";

    private bool isFading = false;
    private float fadeTimer = 0f;

    private float survivalTime = 0f;
    private bool isGameOver = false;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Hides the panel at start
    private void Start()
    {
        if (deadScreenUI != null) deadScreenUI.SetActive(false);
    }

    // Counts up survival time while alive (refreshing the always-visible HUD text), and fades the red
    // background in while dying
    private void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
            if (liveTimerText != null) liveTimerText.text = FormatSurvivalTime(survivalTime);
        }

        if (!isFading) return;

        fadeTimer += Time.deltaTime;
        float t = Mathf.Clamp01(fadeTimer / fadeDuration);

        if (redBackground != null)
        {
            Color c = redBackground.color;
            c.a = Mathf.Lerp(0f, targetAlpha, t);
            redBackground.color = c;
        }

        if (t >= 1f) isFading = false;
    }

    // Shows the Game Over panel, unlocks the cursor, disables player control, and freezes/shows the survival time
    public void Show()
    {
        isGameOver = true;

        if (deadScreenUI != null) deadScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        if (timerHUD != null) timerHUD.SetActive(false);
        if (survivalTimeText != null) survivalTimeText.text = FormatSurvivalTime(survivalTime);

        if (redBackground != null)
        {
            Color c = redBackground.color;
            c.a = 0f;
            redBackground.color = c;
        }
        fadeTimer = 0f;
        isFading = true;

        if (playerMovement != null) playerMovement.enabled = false;
        if (mouseMovement != null) mouseMovement.enabled = false;
        if (playerAttack != null) playerAttack.enabled = false;
        if (hotbarSelection != null) hotbarSelection.enabled = false;
        if (potInteraction != null) potInteraction.enabled = false;
    }

    // Reloads the current scene (fresh player stats, no need to manually re-enable anything)
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Loads the main menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // Formats seconds as "MM:SS" for the survival time display
    private string FormatSurvivalTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        return $"Survived: {minutes:00}:{secs:00}";
    }
}
