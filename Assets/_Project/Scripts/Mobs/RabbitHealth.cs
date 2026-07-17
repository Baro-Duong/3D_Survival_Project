using UnityEngine;

// Tracks a rabbit's HP; aggros on hit and drops meat on death
public class RabbitHealth : MonoBehaviour
{
    public GameConfig config;

    public float currentHP;
    public string meatItemName = "RawMeat";
    public bool isAggressive = false;

    // Sets starting HP from config
    private void Start()
    {
        currentHP = config.rabbitMaxHP;
    }

    // Applies damage, marks the rabbit as aggressive, and kills it if HP drops to 0
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        isAggressive = true;
        if (currentHP <= 0) Die();
    }

    // Drops a meat world item and destroys the rabbit
    private void Die()
    {
        GameObject meatPrefab = Resources.Load<GameObject>("WorldItems/" + meatItemName);
        if (meatPrefab != null)
            Instantiate(meatPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        Destroy(gameObject);
    }
}
