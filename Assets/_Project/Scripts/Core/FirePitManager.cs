using UnityEngine;

// Gắn vào FirePit GameObject trong scene
public class FirePitManager : MonoBehaviour
{
    public enum FirePitState { Normal, Boiling, BoiledWater }

    public FirePitState state = FirePitState.Normal;

    [Header("Prefabs World")]
    public GameObject boilingFirePitPrefab;   // BoillingWaterFirePit
    public GameObject boiledFirePitPrefab;    // BoilledWaterFirePit
    public GameObject firePitPrefab;          // FirePit gốc
    public GameObject potWorldPrefab;         // Pot world item drop ra

    private float boilTimer = 0f;
    private float boilDuration = 30f;
    private int scoopCount = 0;
    private int maxScoops = 3;
    private float potEjectForce = 5f;

    private void Update()
    {
        if (state == FirePitState.Boiling)
        {
            boilTimer += Time.deltaTime;
            if (boilTimer >= boilDuration)
            {
                boilTimer = 0f;
                TransitionTo(FirePitState.BoiledWater);
            }
        }
    }

    public void StartBoiling()
    {
        state = FirePitState.Boiling;
        boilTimer = 0f;
        scoopCount = 0;

        // Thay thế GameObject này bằng BoillingWaterFirePit
        SpawnReplacement(boilingFirePitPrefab);
    }

    public void ScoopWater()
    {
        scoopCount++;
        if (scoopCount >= maxScoops)
        {
            // Hết nước → trở về FirePit bình thường, drop Pot
            state = FirePitState.Normal;
            Vector3 pos = transform.position;
            SpawnReplacement(firePitPrefab);

            // Drop Pot ra world
            if (potWorldPrefab != null)
            {
                GameObject pot = Instantiate(potWorldPrefab, pos + Vector3.up * 2f, Quaternion.identity);
                Rigidbody rb = pot.GetComponent<Rigidbody>();
                if (rb == null) rb = pot.AddComponent<Rigidbody>();
                // Gán thẳng vận tốc thay vì AddForce — không phụ thuộc mass, luôn bay lên đúng tốc độ mong muốn
                rb.linearVelocity = Vector3.up * potEjectForce;
            }
        }
    }

    private void TransitionTo(FirePitState newState)
    {
        state = newState;
        if (newState == FirePitState.BoiledWater)
            SpawnReplacement(boiledFirePitPrefab);
    }

    private void SpawnReplacement(GameObject prefab)
    {
        if (prefab == null) { Debug.LogError("Prefab null trong FirePitManager!"); return; }
        GameObject replacement = Instantiate(prefab, transform.position, transform.rotation);
        // Chuyển state sang replacement nếu cần
        FirePitManager newFP = replacement.GetComponent<FirePitManager>();
        if (newFP != null)
        {
            newFP.state = state;
            newFP.scoopCount = scoopCount;
            newFP.boilingFirePitPrefab = boilingFirePitPrefab;
            newFP.boiledFirePitPrefab = boiledFirePitPrefab;
            newFP.firePitPrefab = firePitPrefab;
            newFP.potWorldPrefab = potWorldPrefab;
        }
        else
        {
            Debug.LogError(prefab.name + " thiếu component FirePitManager — state/scoop bị mất, object sẽ không interact được nữa!");
        }
        Destroy(gameObject);
    }
}