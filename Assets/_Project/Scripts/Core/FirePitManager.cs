using UnityEngine;

// Attached to the FirePit GameObject in the scene; drives the Normal -> Boiling -> BoiledWater state machine
public class FirePitManager : MonoBehaviour
{
    public enum FirePitState { Normal, Boiling, BoiledWater }

    public FirePitState state = FirePitState.Normal;
    public GameConfig config;

    public int uses = -1; // -1 = not yet initialized; Start() fills it from config on first placement only

    private float boilTimer = 0f;
    private float boilDuration = 30f;
    private int scoopCount = 0;
    private int maxScoops = 3;
    private float potEjectForce = 5f;

    // Initializes uses from config, but only the very first time (SpawnReplacement copies it forward
    // on every subsequent transition, so this must not overwrite an already-carried-over value)
    private void Start()
    {
        if (uses < 0) uses = config.firePitMaxUses;
    }

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

    // Starts boiling: consumes the boil-use cost, switches state, and swaps the visual to the boiling prefab
    public void StartBoiling()
    {
        // Clamp at 0 (never negative) — a negative value would be misread by the next replacement's
        // Start() as "uninitialized" (uses < 0 sentinel) and get reset back to firePitMaxUses
        uses = Mathf.Max(0, uses - config.firePitBoilUseCost);

        state = FirePitState.Boiling;
        boilTimer = 0f;
        scoopCount = 0;

        SpawnReplacement(ReferenceManager.Instance.boilingFirePitPrefab);
    }

    // Called each time the player scoops water; after the 3rd scoop, reverts to Normal (or breaks if out
    // of uses) and drops a Pot either way
    public void ScoopWater()
    {
        scoopCount++;
        if (scoopCount >= maxScoops)
        {
            state = FirePitState.Normal;

            if (uses <= 0)
                Destroy(gameObject); // worn out — gone for good
            else
                SpawnReplacement(ReferenceManager.Instance.firePitPrefab);

            EjectPot();
        }
    }

    // Consumes the cook-use cost after successfully cooking meat; breaks the FirePit if it runs out.
    // If it breaks while a Pot is "inside" (Boiling/BoiledWater state), eject the Pot so it isn't lost.
    public void ConsumeCookUse()
    {
        uses = Mathf.Max(0, uses - config.firePitCookUseCost);
        if (uses <= 0)
        {
            if (state == FirePitState.Boiling || state == FirePitState.BoiledWater)
                EjectPot();
            Destroy(gameObject);
        }
    }

    // Spawns a Pot world item above the FirePit and launches it upward (mass-independent velocity, not AddForce impulse)
    private void EjectPot()
    {
        GameObject potWorldPrefab = ReferenceManager.Instance.potWorldPrefab;
        if (potWorldPrefab == null) return;

        GameObject pot = Instantiate(potWorldPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
        Rigidbody rb = pot.GetComponent<Rigidbody>();
        if (rb == null) rb = pot.AddComponent<Rigidbody>();
        rb.linearVelocity = Vector3.up * potEjectForce;
    }

    // Feeds Stick into the FirePit as makeshift fuel, restoring uses (clamped at firePitMaxUses)
    public void AddUses(int amount)
    {
        uses = Mathf.Min(config.firePitMaxUses, uses + amount);
    }

    // Switches state and, if entering BoiledWater, swaps the visual to the boiled prefab
    private void TransitionTo(FirePitState newState)
    {
        state = newState;
        if (newState == FirePitState.BoiledWater)
            SpawnReplacement(ReferenceManager.Instance.boiledFirePitPrefab);
    }

    // Destroys this GameObject and instantiates the given prefab in its place, carrying state/scoopCount/uses
    // forward — the 4 prefab references themselves live once on ReferenceManager, not per-instance, so there's
    // nothing left to copy (and nothing left that can end up self-referencing a scene instance)
    private void SpawnReplacement(GameObject prefab)
    {
        if (prefab == null) { Debug.LogError("Prefab is null in FirePitManager!"); return; }
        GameObject replacement = Instantiate(prefab, transform.position, transform.rotation);

        FirePitManager newFP = replacement.GetComponent<FirePitManager>();
        if (newFP != null)
        {
            newFP.state = state;
            newFP.scoopCount = scoopCount;
            newFP.config = config;
            newFP.uses = uses;
        }
        else
        {
            Debug.LogError(prefab.name + " is missing a FirePitManager component — state/scoop count is lost, the object will no longer be interactable!");
        }
        Destroy(gameObject);
    }
}
