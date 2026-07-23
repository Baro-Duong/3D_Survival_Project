using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// One slot (Input1 / Input2 / Output) inside the crafting UI; forwards drag events to its child item
public class CraftingSlot : MonoBehaviour, IDropHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum SlotType { Input1, Input2, Output }
    public SlotType slotType;

    public TMP_Text stackText; // stack count text, drag into the Inspector (optional)

    // Returns the child GameObject that has an ItemData component (skips StackText), or null if empty
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

    // Returns the current item's clean name (no "(Clone)" suffix), or "" if empty
    public string ItemName
    {
        get
        {
            if (Item != null)
                return Item.name.Replace("(Clone)", "").Trim();
            return "";
        }
    }

    private DragDrop GetChildDragDrop() => Item != null ? Item.GetComponent<DragDrop>() : null;

    // Not used directly; the child item's own DragDrop handles pointer-down
    public void OnPointerDown(PointerEventData eventData) { }

    // Forwards begin-drag to the child item's DragDrop (does nothing if the slot is empty)
    public void OnBeginDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnBeginDrag(eventData);
        // Deliberately no else-branch here — ExecuteEvents.ExecuteHierarchy(gameObject, ...) would
        // re-dispatch IBeginDragHandler back onto this same CraftingSlot, causing infinite recursion
        // (StackOverflowException). Don't reintroduce it.
    }

    // Forwards drag to the child item's DragDrop
    public void OnDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnDrag(eventData);
    }

    // Forwards end-drag to the child item's DragDrop
    public void OnEndDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnEndDrag(eventData);
    }

    // Not used directly; DragDrop.OnEndDrag handles the actual drop logic
    public void OnDrop(PointerEventData eventData) { }

    // Destroys whatever item is currently in this slot
    public void ClearSlot()
    {
        if (Item != null)
            Destroy(Item);
    }

    // Shows/updates the stack count text, or hides it for empty slots and non-stackable items
    public void RefreshStackDisplay()
    {
        if (stackText == null) return;

        // Newly added items are parented as the last sibling, which renders on top and would cover
        // stackText — keep stackText last so it always renders above the item icon
        stackText.transform.SetAsLastSibling();

        if (Item != null)
        {
            ItemData data = Item.GetComponent<ItemData>();
            if (data != null && data.maxDurability > 0)
            {
                stackText.text = data.currentDurability.ToString();
                stackText.gameObject.SetActive(true);
            }
            else if (data != null && data.maxStack > 1)
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
