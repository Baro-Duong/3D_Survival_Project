using UnityEngine;

// Singleton holding the Canvas reference that DragDrop needs to reparent items during drag
public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; set; }

    public Canvas canvas;

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
