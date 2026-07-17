using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to any world object that should show a name label and/or be picked up into inventory
public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;
    public string ItemName;
    public bool isPickupable = true; // turn off for name-only objects (FirePit, trees...) that must not be picked up/destroyed

    // Returns this object's display name (used by SelectionManager for the interaction text)
    public string GetItemName()
    {
        return ItemName;
    }

    // On left-click while targeted and in range, adds this item to inventory and destroys it
    private void Update()
    {
        if (!isPickupable) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.Mouse0) && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddToInvetory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }

    // Marks the player as in range when they enter this object's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // Marks the player as out of range when they leave this object's trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
