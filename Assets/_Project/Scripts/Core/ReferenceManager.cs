using UnityEngine;

// Singleton holding shared references (Canvas for DragDrop, FirePit prefabs) so other scripts don't
// each need their own copy — set these fields once here instead of on every FirePitManager instance
public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; set; }

    public Canvas canvas;

    [Header("FirePit Prefabs")]
    public GameObject firePitPrefab;          // original empty FirePit
    public GameObject boilingFirePitPrefab;   // BoillingWaterFirePit
    public GameObject boiledFirePitPrefab;    // BoilledWaterFirePit
    public GameObject potWorldPrefab;         // Pot world item dropped when water runs out

    [Header("Bush Prefabs")]
    public GameObject bushWithBerriesPrefab;
    public GameObject bushEmptyPrefab;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Returns the shared Canvas
    public Canvas GetCanvasReference()
    {
        return canvas;
    }
}
