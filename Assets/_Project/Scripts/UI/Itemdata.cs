using UnityEngine;

// Attached to every item UI prefab; holds identity, stacking, and consumable info
public class ItemData : MonoBehaviour
{
    [Header("Item Info")]
    public string itemName;
    public int maxStack = 1;
    public int currentStack = 1;

    [Header("Consumable")]
    public bool isConsumable = false;
    public float hungerRestore = 0f;
    public float thirstRestore = 0f;

    [Header("Durability (tools only, 0 = not a durability item)")]
    public int maxDurability = 0;
    public int currentDurability = 0;
}
