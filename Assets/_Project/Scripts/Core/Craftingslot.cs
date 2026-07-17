using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum SlotType { Input1, Input2, Output }
    public SlotType slotType;

    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            return null;
        }
    }

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

    // Forward tất cả drag event xuống item con
    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnBeginDrag(eventData);
        else ExecuteEvents.ExecuteHierarchy(gameObject, eventData, ExecuteEvents.beginDragHandler);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var dd = GetChildDragDrop();
        if (dd != null) dd.OnEndDrag(eventData);
    }

    public void OnDrop(PointerEventData eventData) { }

    public void ClearSlot()
    {
        if (Item != null)
            Destroy(Item);
    }
}