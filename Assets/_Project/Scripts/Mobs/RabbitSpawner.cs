using UnityEngine;
using UnityEngine.SceneManagement;

// Attached to each RabbitHole GameObject; periodically tops up the rabbit population on the map.
// Also handles the boss rabbit: at most one may exist at a time, and after one is killed a cooldown of
// bossSpawnCycles spawn cycles must elapse before the next one appears.
public class RabbitSpawner : MonoBehaviour
{
    public GameConfig config;
    public GameObject rabbitPrefab;
    public GameObject bossRabbitPrefab;

    [Header("Debug")]
    public bool logSpawnChecks = false; // tick to trace spawn decisions in the Console

    // The boss is a single map-wide entity, so its cooldown is shared by every burrow. It is tracked as
    // a timestamp rather than a counter: each burrow runs its own timer, so a counter would advance once
    // per burrow per cycle and the required total would silently depend on how many burrows the scene has.
    private static float lastBossEventTime = 0f;

    private float checkTimer = 0f;

    // Static fields survive a scene load, so the timestamp has to be re-based whenever a scene loads —
    // otherwise restarting from the death screen would carry the previous run's progress over.
    // Registered once per play session, before the first scene loads.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void HookSceneReset()
    {
        SceneManager.sceneLoaded += (scene, mode) => lastBossEventTime = Time.time;
    }

    // Called by RabbitHealth when a boss dies, restarting the cooldown before the next one
    public static void OnBossKilled()
    {
        lastBossEventTime = Time.time;
    }

    // Counts down and checks the rabbit population once the interval elapses
    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= config.rabbitSpawnCheckInterval)
        {
            checkTimer = 0f;
            CheckAndSpawn();
        }
    }

    // Spawns the boss if one is due and none is alive, otherwise tops up the ordinary rabbit population
    private void CheckAndSpawn()
    {
        RabbitHealth[] rabbits = FindObjectsByType<RabbitHealth>(FindObjectsSortMode.None);

        bool bossAlive = false;
        foreach (RabbitHealth rabbit in rabbits)
        {
            if (rabbit.isBoss) { bossAlive = true; break; }
        }

        float bossCooldown = config.bossSpawnCycles * config.rabbitSpawnCheckInterval;
        float elapsed = Time.time - lastBossEventTime;

        if (logSpawnChecks)
        {
            string bossInfo = bossAlive ? "boss alive" : $"boss in {Mathf.Max(0f, bossCooldown - elapsed):0.0}s";
            Debug.Log($"[RabbitSpawner] {name}: rabbits {rabbits.Length}/{config.maxRabbitsOnMap}, {bossInfo}");
        }

        if (!bossAlive && elapsed >= bossCooldown)
        {
            // The boss ignores the population cap; otherwise a full map would block it indefinitely
            if (Spawn(bossRabbitPrefab, "bossRabbitPrefab"))
                lastBossEventTime = Time.time; // also covers a boss that dies without being killed
            return;
        }

        if (rabbits.Length >= config.maxRabbitsOnMap) return;
        Spawn(rabbitPrefab, "rabbitPrefab");
    }

    // Instantiates the given prefab at this hole's position; returns false if the prefab is unassigned
    private bool Spawn(GameObject prefab, string fieldName)
    {
        if (prefab == null)
        {
            Debug.LogError("RabbitSpawner: " + fieldName + " is not assigned!");
            return false;
        }

        Instantiate(prefab, transform.position, Quaternion.identity);
        return true;
    }
}
