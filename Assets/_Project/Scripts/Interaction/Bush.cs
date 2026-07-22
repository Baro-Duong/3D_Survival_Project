using UnityEngine;

// Attached to a harvestable bush. Interacting with it while it HasBerries gives 1 Stick + a batch of
// Berries at once and swaps to the Empty prefab; it regrows back to HasBerries after berryRegrowTime
public class Bush : MonoBehaviour
{
    public enum BushState { HasBerries, Empty }
    public BushState state = BushState.HasBerries;

    public GameConfig config;

    private float regrowTimer = 0f;

    // Counts down the regrow timer while Empty and swaps back to HasBerries once it elapses
    private void Update()
    {
        if (state != BushState.Empty) return;

        regrowTimer += Time.deltaTime;
        if (regrowTimer >= config.berryRegrowTime)
        {
            regrowTimer = 0f;
            SpawnReplacement(ReferenceManager.Instance.bushWithBerriesPrefab);
        }
    }

    // If the bush currently has berries, harvests them (1 Stick + berriesPerHarvest Berries) and empties it
    public void TryHarvest()
    {
        if (state != BushState.HasBerries) return;

        InventorySystem.Instance.AddToInvetory("Stick");
        for (int i = 0; i < config.berriesPerHarvest; i++)
            InventorySystem.Instance.AddToInvetory("Berry");

        state = BushState.Empty;
        regrowTimer = 0f;
        SpawnReplacement(ReferenceManager.Instance.bushEmptyPrefab);
    }

    // Destroys this GameObject and instantiates the given prefab in its place, carrying state/timer forward.
    // The 2 prefab references live once on ReferenceManager (not per-instance) — same fix as FirePitManager,
    // to avoid the self-referencing-prefab-field bug class.
    private void SpawnReplacement(GameObject prefab)
    {
        if (prefab == null) { Debug.LogError("Prefab is null in Bush!"); return; }
        GameObject replacement = Instantiate(prefab, transform.position, transform.rotation);

        Bush newBush = replacement.GetComponent<Bush>();
        if (newBush != null)
        {
            newBush.state = state;
            newBush.regrowTimer = regrowTimer;
        }
        else
        {
            Debug.LogError(prefab.name + " is missing a Bush component — state will reset!");
        }

        Destroy(gameObject);
    }
}
