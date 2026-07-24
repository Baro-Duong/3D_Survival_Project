using UnityEngine;

// Attached to a minable big rock; tracks pickaxe hits and drops a Rock every N hits (like Tree/Axe).
// Also periodically pops out a bonus Rock on its own — a safety net so the player can never fully run
// out of Rock even after spending it all crafting — launched upward like the Pot from
// FirePitManager.ScoopWater().
public class BigRock : MonoBehaviour
{
    public GameConfig config;

    private int hitCount = 0;
    private float bonusSpawnTimer = 0f;
    private float rockEjectForce = 5f; // matches FirePitManager.potEjectForce

    // Counts down the bonus-spawn timer independently of mining hits
    private void Update()
    {
        bonusSpawnTimer += Time.deltaTime;
        if (bonusSpawnTimer >= config.rockBonusSpawnInterval)
        {
            bonusSpawnTimer = 0f;
            DropRock(true);
        }
    }

    // Registers 1 pickaxe hit; every hitsPerRock-th hit drops a Rock at the base of the rock
    public void Mine()
    {
        hitCount++;
        if (hitCount >= config.hitsPerRock)
        {
            hitCount = 0;
            DropRock(false);
        }
    }

    // Spawns a Rock world item. A mined drop just appears at the base (like Tree's Stick); the periodic
    // bonus spawn launches it straight up (mass-independent velocity, not AddForce impulse)
    private void DropRock(bool launchUpward)
    {
        GameObject rockPrefab = Resources.Load<GameObject>("WorldItems/Rock");
        if (rockPrefab == null)
        {
            Debug.LogError("Rock world prefab not found in Resources/WorldItems!");
            return;
        }

        GameObject rock = Instantiate(rockPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);

        if (launchUpward)
        {
            Rigidbody rb = rock.GetComponent<Rigidbody>();
            if (rb == null) rb = rock.AddComponent<Rigidbody>();
            rb.linearVelocity = Vector3.up * rockEjectForce;
        }
    }
}
