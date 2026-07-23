using UnityEngine;

// Central data asset holding every tunable gameplay number; scripts read config.field instead of hardcoding
[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    // ==================== PLAYER MOVEMENT ====================
    [Header("Player Movement")]
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float jumpHeight = 3f;
    public float gravity = -19.62f;
    public float groundDistance = 0.4f;

    // ==================== MOUSE ====================
    [Header("Mouse")]
    public float mouseSensitivity = 100f;

    // ==================== PLAYER STATS ====================
    [Header("Player Stats - Max Values")]
    public float maxHP = 100f;
    public float maxThirst = 100f;
    public float maxHunger = 100f;

    [Header("Player Stats - Drain Rates")]
    public float thirstDrainRate = 1f;
    public float thirstSprintBonus = 2f;
    public float hungerDrainRate = 0.3f;

    [Header("Player Stats - HP Drain")]
    public float hpDrainWhenNoThirst = 3f;
    public float hpDrainWhenNoHunger = 1f;

    [Header("Player Stats - HP Regen")]
    public float hpRegenRate = 5f;
    public float hpRegenThreshold = 50f; // Thirst AND Hunger must both be above this to regen
    public float thirstDrainRegenBonus = 0.5f; // extra drain while actively regenerating
    public float hungerDrainRegenBonus = 0.15f;

    // ==================== PLAYER COMBAT ====================
    [Header("Player Combat")]
    public float attackDamage = 2f;       // bare hands (or any non-Axe item)
    public float axeAttackDamage = 5f;    // while holding an Axe (also consumes 1 durability)
    public float attackRange = 3f;
    public float attackCooldown = 0.5f;

    // ==================== HOTBAR / DROP ====================
    [Header("Item Drop")]
    public float dropForce = 5f;

    // ==================== TREE CHOPPING ====================
    [Header("Tree Chopping")]
    public int chopsPerStick = 2;

    // ==================== BUSH ====================
    [Header("Bush")]
    public int berriesPerHarvest = 5;
    public float berryRegrowTime = 120f;

    // ==================== FIREPIT DURABILITY ====================
    [Header("FirePit Durability")]
    public int firePitMaxUses = 50;
    public int firePitBoilUseCost = 10;
    public int firePitCookUseCost = 1;
    public float cookRequiredTime = 10f; // seconds holding F to turn RawMeat into CookedMeat

    // ==================== RABBIT ====================
    [Header("Rabbit Stats")]
    public float rabbitMaxHP = 10f;

    [Header("Rabbit Movement")]
    public float rabbitMoveSpeed = 0.2f;
    public float rabbitWalkTimeMin = 3f;
    public float rabbitWalkTimeMax = 6f;
    public float rabbitWaitTimeMin = 5f;
    public float rabbitWaitTimeMax = 7f;

    [Header("Rabbit Attack")]
    public float rabbitChaseSpeed = 1.5f;
    public float rabbitAttackRange = 1.2f;
    public float rabbitAttackDamage = 5f;
    public float rabbitAttackCooldown = 1f;

    [Header("Rabbit Spawning")]
    public int maxRabbitsOnMap = 10;
    public float rabbitSpawnCheckInterval = 60f;

    // ==================== ITEMS ====================
    [Header("Apple")]
    public float appleHungerRestore = 10f;
    public float appleThirstRestore = 5f;
    public int appleMaxStack = 10;

    [Header("Rock / Stick")]
    public int rockMaxStack = 10;
    public int stickMaxStack = 10;
}