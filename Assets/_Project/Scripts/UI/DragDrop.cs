using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Attached to every item UI prefab; implements dragging an item between inventory/crafting slots
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    // Caches components and grabs the shared Canvas reference
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = ReferenceManager.Instance.GetCanvasReference();
    }

    // Not used; drag start is handled in OnBeginDrag
    public void OnPointerDown(PointerEventData eventData) { }

    // Fades the item, lets raycasts pass through it, and reparents it to the Canvas so it renders on top while dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(canvas.transform);
        itemBeingDragged = gameObject;

        // Refresh the old slot right away (hides its stack text since it's now empty)
        ItemSlot oldSlot = startParent.GetComponent<ItemSlot>();
        if (oldSlot != null) oldSlot.RefreshStackDisplay();

        CraftingSlot oldCraftingSlot = startParent.GetComponent<CraftingSlot>();
        if (oldCraftingSlot != null) oldCraftingSlot.RefreshStackDisplay();
    }

    // Moves the item to follow the pointer
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Drops the item into the crafting slot under the pointer, or returns it to its original slot
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        CraftingSlot targetSlot = FindSlotUnderPointer(eventData);

        if (targetSlot != null && targetSlot.slotType != CraftingSlot.SlotType.Output && targetSlot.Item == null)
        {
            transform.SetParent(targetSlot.transform);
            transform.localPosition = Vector2.zero;
        }
        else if (targetSlot != null && targetSlot.slotType == CraftingSlot.SlotType.Output)
        {
            // Can't drop into the output slot: send it back
            transform.SetParent(startParent);
            transform.position = startPosition;
        }
        else if (transform.parent == canvas.transform)
        {
            // Didn't land on any slot: send it back
            transform.SetParent(startParent);
            transform.position = startPosition;
        }

        CraftingSystem.Instance.CheckRecipe();

        // Refresh the slot it landed in
        ItemSlot newSlot = transform.parent.GetComponent<ItemSlot>();
        if (newSlot != null) newSlot.RefreshStackDisplay();

        CraftingSlot newCraftingSlot = transform.parent.GetComponent<CraftingSlot>();
        if (newCraftingSlot != null) newCraftingSlot.RefreshStackDisplay();

        itemBeingDragged = null;
    }

    // Finds the CraftingSlot (if any) under the current pointer position
    private CraftingSlot FindSlotUnderPointer(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            CraftingSlot slot = result.gameObject.GetComponent<CraftingSlot>();
            if (slot != null) return slot;

            slot = result.gameObject.GetComponentInParent<CraftingSlot>();
            if (slot != null) return slot;
        }
        return null;
    }
}
