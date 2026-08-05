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
    public float attackDamage = 2f;       // bare hands (or any non-tool item)
    public float toolAttackDamage = 5f;   // while holding an Axe or Pickaxe (also consumes 1 durability)
    public float attackRange = 3f;
    public float attackCooldown = 0.5f;

    // ==================== DAMAGE FLASH ====================
    [Header("Damage Flash")]
    public float damageFlashAlpha = 0.25f;      // peak red overlay opacity when hit
    public float damageFlashFadeDuration = 0.4f; // seconds to fade back to transparent
    public float damageFlashIntensityPerHP = 0.1f; // extra multiplier per HP lost in a single hit (5 HP -> 1.5x)

    // ==================== HOTBAR / DROP ====================
    [Header("Item Drop")]
    public float dropForce = 5f;

    // ==================== TREE CHOPPING ====================
    [Header("Tree Chopping")]
    public int chopsPerStick = 2;
    public int chopsPerApple = 10; // every Nth chop also drops an Apple, launched straight up

    // ==================== BUSH ====================
    [Header("Bush")]
    public int berriesPerHarvest = 5;
    public float berryRegrowTime = 120f;

    // ==================== BIG ROCK / PICKAXE MINING ====================
    [Header("Big Rock Mining")]
    public int hitsPerRock = 2;
    public float rockBonusSpawnInterval = 180f; // safety-net Rock, independent of mining hits

    // ==================== FIREPIT DURABILITY ====================
    [Header("FirePit Durability")]
    public int firePitMaxUses = 50;
    public int firePitBoilUseCost = 10;
    public int firePitCookUseCost = 1;
    public float cookRequiredTime = 10f; // seconds holding F to turn RawMeat into CookedMeat
    public int stickRepairUses = 2; // uses restored per Stick fed into the FirePit (clamped at firePitMaxUses)
    public int rockRepairUses = 5;  // uses restored per Rock fed into the FirePit (clamped at firePitMaxUses)

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

    [Header("Boss Rabbit")]
    public float bossStatMultiplier = 2f;           // multiplies max HP, attack damage and chase speed
    public float bossDetectionRangeMultiplier = 2f; // auto-aggro radius = rabbitAttackRange * this
    public int bossSpawnCycles = 3;                 // spawn cycles that must pass after a boss dies before the next one
                                                    // (delay = bossSpawnCycles * rabbitSpawnCheckInterval seconds,
                                                    //  so it stays correct no matter how many burrows exist)
    public int bossMeatDrop = 2;                    // meat items dropped by a boss (normal rabbits drop 1)
}