using UnityEngine;

// Tracks a rabbit's HP; aggros on hit and drops meat on death.
// Boss rabbits use the same script with isBoss ticked: they get multiplied stats and, unlike normal
// rabbits, turn hostile on their own once the player comes close (handled in AI_Movement).
public class RabbitHealth : MonoBehaviour
{
    public GameConfig config;

    [Header("Boss")]
    public bool isBoss = false; // tick this on the boss prefab only

    public float currentHP;
    public string meatItemName = "RawMeat";
    public bool isAggressive = false;

    // Returns the stat multiplier for this rabbit (1 for normal rabbits, config value for the boss)
    public float StatMultiplier => isBoss ? config.bossStatMultiplier : 1f;

    // Max HP after the boss multiplier — read this instead of config.rabbitMaxHP anywhere the full
    // health total is needed (e.g. the HP readout), otherwise a boss shows the base value
    public float MaxHP => config.rabbitMaxHP * StatMultiplier;

    // Name shown by SelectionManager when the player looks at this rabbit
    public string DisplayName => isBoss ? "Alpha Rabbit" : "Rabbit";

    // Sets starting HP from config, scaled up for the boss
    private void Start()
    {
        currentHP = config.rabbitMaxHP * StatMultiplier;
    }

    // Applies damage, marks the rabbit as aggressive, and kills it if HP drops to 0
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        isAggressive = true;
        if (currentHP <= 0) Die();
    }

    // Drops meat world items and destroys the rabbit; a boss drops more and restarts the boss spawn counter
    private void Die()
    {
        GameObject meatPrefab = Resources.Load<GameObject>("WorldItems/" + meatItemName);
        if (meatPrefab != null)
        {
            int dropCount = isBoss ? config.bossMeatDrop : 1;
            for (int i = 0; i < dropCount; i++)
            {
                // Spread the drops slightly so they don't spawn inside each other and get pushed away
                Vector3 offset = new Vector3(Random.Range(-0.3f, 0.3f), 0.5f, Random.Range(-0.3f, 0.3f));
                Instantiate(meatPrefab, transform.position + offset, Quaternion.identity);
            }
        }

        if (isBoss) RabbitSpawner.OnBossKilled();

        Destroy(gameObject);
    }
}
