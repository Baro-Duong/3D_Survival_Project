using UnityEngine;

// Attached to any choppable tree; tracks chop count and drops a Stick every chopsPerStick chops, plus
// an Apple launched straight up every chopsPerApple chops
public class Tree : MonoBehaviour
{
    public GameConfig config;

    private int chopCount = 0;
    private int totalChops = 0;
    private float appleEjectForce = 5f; // matches FirePitManager.potEjectForce / BigRock.rockEjectForce

    // Registers 1 chop; every chopsPerStick-th chop drops a Stick, every chopsPerApple-th chop also drops an Apple
    public void Chop()
    {
        chopCount++;
        totalChops++;

        if (chopCount >= config.chopsPerStick)
        {
            chopCount = 0;
            DropStick();
        }

        if (totalChops % config.chopsPerApple == 0)
            DropApple();
    }

    // Spawns a Stick world item at the base of the tree
    private void DropStick()
    {
        GameObject stickPrefab = Resources.Load<GameObject>("WorldItems/Stick");
        if (stickPrefab == null)
        {
            Debug.LogError("Stick world prefab not found in Resources/WorldItems!");
            return;
        }

        Instantiate(stickPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
    }

    // Spawns an Apple world item and launches it straight up (mass-independent velocity, not AddForce impulse)
    private void DropApple()
    {
        GameObject applePrefab = Resources.Load<GameObject>("WorldItems/Apple");
        if (applePrefab == null)
        {
            Debug.LogError("Apple world prefab not found in Resources/WorldItems!");
            return;
        }

        GameObject apple = Instantiate(applePrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
        Rigidbody rb = apple.GetComponent<Rigidbody>();
        if (rb == null) rb = apple.AddComponent<Rigidbody>();
        rb.linearVelocity = Vector3.up * appleEjectForce;
    }
}
