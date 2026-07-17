using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvas = ReferenceManager.Instance.GetCanvasReference();
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(canvas.transform);
        itemBeingDragged = gameObject;

        // Refresh slot cũ ngay khi bắt đầu kéo (ẩn stack text)
        ItemSlot oldSlot = startParent.GetComponent<ItemSlot>();
        if (oldSlot != null) oldSlot.RefreshStackDisplay();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Tìm CraftingSlot nằm dưới vị trí thả
        CraftingSlot targetSlot = FindSlotUnderPointer(eventData);

        if (targetSlot != null && targetSlot.slotType != CraftingSlot.SlotType.Output && targetSlot.Item == null)
        {
            transform.SetParent(targetSlot.transform);
            transform.localPosition = Vector2.zero;
        }
        else if (targetSlot != null && targetSlot.slotType == CraftingSlot.SlotType.Output)
        {
            // Thả vào output → trả về chỗ cũ
            transform.SetParent(startParent);
            transform.position = startPosition;
        }
        else if (transform.parent == canvas.transform)
        {
            // Không trúng slot nào → trả về chỗ cũ
            transform.SetParent(startParent);
            transform.position = startPosition;
        }

        // Check recipe sau mỗi lần drag
        CraftingSystem.Instance.CheckRecipe();

        // Refresh slot mới sau khi drop
        ItemSlot newSlot = transform.parent.GetComponent<ItemSlot>();
        if (newSlot != null) newSlot.RefreshStackDisplay();

        itemBeingDragged = null;
    }

    private CraftingSlot FindSlotUnderPointer(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            CraftingSlot slot = result.gameObject.GetComponent<CraftingSlot>();
            if (slot != null) return slot;

            // Check parent cũng có CraftingSlot không
            slot = result.gameObject.GetComponentInParent<CraftingSlot>();
            if (slot != null) return slot;
        }
        return null;
    }
}