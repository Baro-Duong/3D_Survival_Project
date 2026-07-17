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

    // ==================== PLAYER COMBAT ====================
    [Header("Player Combat")]
    public float attackDamage = 5f;
    public float attackRange = 3f;
    public float attackCooldown = 0.5f;

    // ==================== HOTBAR / DROP ====================
    [Header("Item Drop")]
    public float dropForce = 5f;

    // ==================== RABBIT ====================
    [Header("Rabbit Stats")]
    public float rabbitMaxHP = 12f;

    [Header("Rabbit Movement")]
    public float rabbitMoveSpeed = 0.2f;
    public float rabbitWalkTimeMin = 3f;
    public float rabbitWalkTimeMax = 6f;
    public float rabbitWaitTimeMin = 5f;
    public float rabbitWaitTimeMax = 7f;

    // ==================== ITEMS ====================
    [Header("Apple")]
    public float appleHungerRestore = 10f;
    public float appleThirstRestore = 5f;
    public int appleMaxStack = 10;

    [Header("Rock / Stick")]
    public int rockMaxStack = 10;
    public int stickMaxStack = 10;
}