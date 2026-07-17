using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }

    public GameConfig config;

    public float currentHP;
    public float currentThirst;
    public float currentHunger;

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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

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

    private void Update()
    {
        bool isSprinting = playerMovement != null && playerMovement.isSprinting;

        float thirstDrain = config.thirstDrainRate;
        if (isSprinting) thirstDrain += config.thirstSprintBonus;
        currentThirst = Mathf.Max(0, currentThirst - thirstDrain * Time.deltaTime);

        currentHunger = Mathf.Max(0, currentHunger - config.hungerDrainRate * Time.deltaTime);

        if (currentThirst <= 0)
            currentHP = Mathf.Max(0, currentHP - config.hpDrainWhenNoThirst * Time.deltaTime);

        if (currentHunger <= 0)
            currentHP = Mathf.Max(0, currentHP - config.hpDrainWhenNoHunger * Time.deltaTime);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (hpBar != null) hpBar.value = currentHP / config.maxHP;
        if (thirstBar != null) thirstBar.value = currentThirst / config.maxThirst;
        if (hungerBar != null) hungerBar.value = currentHunger / config.maxHunger;

        if (hpText != null) hpText.text = Mathf.CeilToInt(currentHP) + "/" + (int)config.maxHP;
        if (thirstText != null) thirstText.text = Mathf.CeilToInt(currentThirst) + "/" + (int)config.maxThirst;
        if (hungerText != null) hungerText.text = Mathf.CeilToInt(currentHunger) + "/" + (int)config.maxHunger;
    }

    public void TakeDamage(float amount) => currentHP = Mathf.Max(0, currentHP - amount);
    public void Heal(float amount) => currentHP = Mathf.Min(config.maxHP, currentHP + amount);
    public void DrinkWater(float amount) => currentThirst = Mathf.Min(config.maxThirst, currentThirst + amount);
    public void EatFood(float amount) => currentHunger = Mathf.Min(config.maxHunger, currentHunger + amount);
}