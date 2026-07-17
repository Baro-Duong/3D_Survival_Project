using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// A single inventory/hotbar slot: exposes the item it holds and handles items dropped onto it
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public TMP_Text stackText; // bottom-right stack count text, drag into the Inspector

    // Returns the child GameObject that has an ItemData component (skips the StackText child), or null if empty
    public GameObject Item
    {
        get
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<ItemData>() != null)
                    return child.gameObject;
            }
            return null;
        }
    }

    // Places the dragged item here if empty, or merges it into the existing stack if it's the same item
    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged == null) return;

        if (!Item)
        {
            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = Vector2.zero;
            RefreshStackDisplay();
        }
        else
        {
            ItemData incomingData = DragDrop.itemBeingDragged.GetComponent<ItemData>();
            ItemData existingData = Item.GetComponent<ItemData>();

            if (incomingData != null && existingData != null
                && incomingData.itemName == existingData.itemName
                && existingData.currentStack < existingData.maxStack)
            {
                int spaceLeft = existingData.maxStack - existingData.currentStack;
                int toAdd = Mathf.Min(incomingData.currentStack, spaceLeft);

                existingData.currentStack += toAdd;
                incomingData.currentStack -= toAdd;

                if (incomingData.currentStack <= 0)
                {
                    Destroy(DragDrop.itemBeingDragged);
                    InventorySystem.Instance.itemList.Remove(incomingData.itemName);
                }

                RefreshStackDisplay();
            }
        }
    }

    // Shows/updates the stack count text, or hides it for empty slots and non-stackable items
    public void RefreshStackDisplay()
    {
        if (stackText == null) return;

        if (Item != null)
        {
            ItemData data = Item.GetComponent<ItemData>();
            if (data != null && data.maxStack > 1)
            {
                stackText.text = data.currentStack.ToString();
                stackText.gameObject.SetActive(true);
            }
            else
            {
                stackText.gameObject.SetActive(false);
            }
        }
        else
        {
            stackText.gameObject.SetActive(false);
        }
    }
}
