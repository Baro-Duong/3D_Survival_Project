using UnityEngine;
using UnityEngine.EventSystems;

// One slot (Input1 / Input2 / Output) inside the crafting UI; forwards drag events to its child item
public class CraftingSlot : MonoBehaviour, IDropHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum SlotType { Input1, Input2, Output }
    public SlotType slotType;

    // Returns the item GameObject currently in this slot, or null if empty
    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
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

    // Forwards begin-drag to the child item's DragDrop
    public void OnBeginDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnBeginDrag(eventData);
        else ExecuteEvents.ExecuteHierarchy(gameObject, eventData, ExecuteEvents.beginDragHandler);
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
}
