using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Tracks HP/Thirst/Hunger, drains them over time, and updates the stat bars
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }

    public GameConfig config;

    public float currentHP;
    public float currentThirst;
    public float currentHunger;
    public bool isDead = false;

    [Header("UI - HP")]
    public Slider hpBar;
    public TMP_Text hpText;

    [Header("UI - Thirst")]
    public Slider thirstBar;
    public TMP_Text thirstText;

    [Header("UI - Hunger")]
    public Slider hungerBar;
    public TMP_Text hungerText;

    private PlayerMovement playerMovement;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Sets starting stats to their max values and finds the PlayerMovement reference (for sprint state)
    private void Start()
    {
        currentHP = config.maxHP;
        currentThirst = config.maxThirst;
        currentHunger = config.maxHunger;

        playerMovement = GetComponentInParent<PlayerMovement>();
        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerMovement>();

        UpdateUI();
    }

    // Drains thirst/hunger over time (faster while sprinting or regenerating HP) and damages/regens HP accordingly
    private void Update()
    {
        bool isSprinting = playerMovement != null && playerMovement.isSprinting;
        bool isRegenerating = currentHP < config.maxHP
            && currentThirst > config.hpRegenThreshold
            && currentHunger > config.hpRegenThreshold;

        float thirstDrain = config.thirstDrainRate;
        if (isSprinting) thirstDrain += config.thirstSprintBonus;
        if (isRegenerating) thirstDrain += config.thirstDrainRegenBonus;
        currentThirst = Mathf.Max(0, currentThirst - thirstDrain * Time.deltaTime);

        float hungerDrain = config.hungerDrainRate;
        if (isRegenerating) hungerDrain += config.hungerDrainRegenBonus;
        currentHunger = Mathf.Max(0, currentHunger - hungerDrain * Time.deltaTime);

        if (currentThirst <= 0)
            TakeDamage(config.hpDrainWhenNoThirst * Time.deltaTime);

        if (currentHunger <= 0)
            TakeDamage(config.hpDrainWhenNoHunger * Time.deltaTime);

        if (isRegenerating)
            currentHP = Mathf.Min(config.maxHP, currentHP + config.hpRegenRate * Time.deltaTime);

        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            if (DeadScreen.Instance != null) DeadScreen.Instance.Show();
        }

        UpdateUI();
    }

    // Refreshes the HP/Thirst/Hunger bars and their text labels
    private void UpdateUI()
    {
        if (hpBar != null) hpBar.value = currentHP / config.maxHP;
        if (thirstBar != null) thirstBar.value = currentThirst / config.maxThirst;
        if (hungerBar != null) hungerBar.value = currentHunger / config.maxHunger;

        if (hpText != null) hpText.text = Mathf.CeilToInt(currentHP) + "/" + (int)config.maxHP;
        if (thirstText != null) thirstText.text = Mathf.CeilToInt(currentThirst) + "/" + (int)config.maxThirst;
        if (hungerText != null) hungerText.text = Mathf.CeilToInt(currentHunger) + "/" + (int)config.maxHunger;
    }

    public void TakeDamage(float amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
        if (DamageFlash.Instance != null) DamageFlash.Instance.Flash(amount);
    }
    public void Heal(float amount) => currentHP = Mathf.Min(config.maxHP, currentHP + amount);
    public void DrinkWater(float amount) => currentThirst = Mathf.Min(config.maxThirst, currentThirst + amount);
    public void EatFood(float amount) => currentHunger = Mathf.Min(config.maxHunger, currentHunger + amount);
}
