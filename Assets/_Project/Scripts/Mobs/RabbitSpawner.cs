using UnityEngine;

// Attached to the RabbitHole GameObject; periodically tops up the rabbit population on the map
public class RabbitSpawner : MonoBehaviour
{
    public GameConfig config;
    public GameObject rabbitPrefab;

    private float checkTimer = 0f;

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

    // Spawns a single rabbit at this hole's position if the map is below its rabbit cap
    private void CheckAndSpawn()
    {
        int currentCount = FindObjectsByType<RabbitHealth>(FindObjectsSortMode.None).Length;
        if (currentCount >= config.maxRabbitsOnMap) return;

        if (rabbitPrefab == null)
        {
            Debug.LogError("RabbitSpawner: rabbitPrefab is not assigned!");
            return;
        }

        Instantiate(rabbitPrefab, transform.position, Quaternion.identity);
    }
}
