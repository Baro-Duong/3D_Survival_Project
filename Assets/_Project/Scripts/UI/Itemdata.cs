using UnityEngine;

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
}