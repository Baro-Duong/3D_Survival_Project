using UnityEngine;

public class RabbitHealth : MonoBehaviour
{
    public GameConfig config;

    public float currentHP;
    public string meatItemName = "RawMeat";
    public bool isAggressive = false;

    private void Start()
    {
        currentHP = config.rabbitMaxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        isAggressive = true;
        if (currentHP <= 0) Die();
    }

    private void Die()
    {
        GameObject meatPrefab = Resources.Load<GameObject>("WorldItems/" + meatItemName);
        if (meatPrefab != null)
            Instantiate(meatPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        Destroy(gameObject);
    }
}