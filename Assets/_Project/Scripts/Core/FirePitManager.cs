using UnityEngine;

// Attached to the FirePit GameObject in the scene; drives the Normal -> Boiling -> BoiledWater state machine
public class FirePitManager : MonoBehaviour
{
    public enum FirePitState { Normal, Boiling, BoiledWater }

    public FirePitState state = FirePitState.Normal;

    [Header("Prefabs World")]
    public GameObject boilingFirePitPrefab;   // BoillingWaterFirePit
    public GameObject boiledFirePitPrefab;    // BoilledWaterFirePit
    public GameObject firePitPrefab;          // original empty FirePit
    public GameObject potWorldPrefab;         // Pot world item dropped when water runs out

    private float boilTimer = 0f;
    private float boilDuration = 30f;
    private int scoopCount = 0;
    private int maxScoops = 3;
    private float potEjectForce = 5f;

    // Advances the boil timer while Boiling and transitions to BoiledWater once it elapses
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

    // Starts boiling: switches state and swaps the visual to the boiling prefab
    public void StartBoiling()
    {
        state = FirePitState.Boiling;
        boilTimer = 0f;
        scoopCount = 0;

        SpawnReplacement(boilingFirePitPrefab);
    }

    // Called each time the player scoops water; after the 3rd scoop, reverts to Normal and drops a Pot
    public void ScoopWater()
    {
        scoopCount++;
        if (scoopCount >= maxScoops)
        {
            state = FirePitState.Normal;
            Vector3 pos = transform.position;
            SpawnReplacement(firePitPrefab);

            if (potWorldPrefab != null)
            {
                GameObject pot = Instantiate(potWorldPrefab, pos + Vector3.up * 2f, Quaternion.identity);
                Rigidbody rb = pot.GetComponent<Rigidbody>();
                if (rb == null) rb = pot.AddComponent<Rigidbody>();
                // Set velocity directly (mass-independent) so the Pot always launches upward at the same speed
                rb.linearVelocity = Vector3.up * potEjectForce;
            }
        }
    }

    // Switches state and, if entering BoiledWater, swaps the visual to the boiled prefab
    private void TransitionTo(FirePitState newState)
    {
        state = newState;
        if (newState == FirePitState.BoiledWater)
            SpawnReplacement(boiledFirePitPrefab);
    }

    // Destroys this GameObject and instantiates the given prefab in its place, carrying state forward
    private void SpawnReplacement(GameObject prefab)
    {
        if (prefab == null) { Debug.LogError("Prefab is null in FirePitManager!"); return; }
        GameObject replacement = Instantiate(prefab, transform.position, transform.rotation);

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
            Debug.LogError(prefab.name + " is missing a FirePitManager component — state/scoop count is lost, the object will no longer be interactable!");
        }
        Destroy(gameObject);
    }
}
