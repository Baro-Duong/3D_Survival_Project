using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Shows the Game Over panel when the player dies: fades in a red overlay, freezes player control,
// and wires up the Restart/Main Menu buttons
public class DeadScreen : MonoBehaviour
{
    public static DeadScreen Instance { get; set; }

    [Header("UI")]
    public GameObject deadScreenUI;
    public Image redBackground;

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

    // Fades the red background in from transparent to targetAlpha over fadeDuration
    private void Update()
    {
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

    // Shows the Game Over panel, unlocks the cursor, and disables player control
    public void Show()
    {
        if (deadScreenUI != null) deadScreenUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

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
}
