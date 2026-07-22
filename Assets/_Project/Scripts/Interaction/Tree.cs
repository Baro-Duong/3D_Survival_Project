using UnityEngine;

// Attached to any choppable tree; tracks chop count and drops a Stick every N chops
public class Tree : MonoBehaviour
{
    public GameConfig config;

    private int chopCount = 0;

    // Registers 1 chop; every chopsPerStick-th chop drops a Stick world item near the tree
    public void Chop()
    {
        chopCount++;
        if (chopCount >= config.chopsPerStick)
        {
            chopCount = 0;
            DropStick();
        }
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
}
