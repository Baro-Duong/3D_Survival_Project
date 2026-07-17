using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public TMP_Text stackText; // text góc dưới phải, kéo vào Inspector

    public GameObject Item
    {
        get
        {
            foreach (Transform child in transform)
            {
                // Bỏ qua StackText, chỉ lấy object có ItemData
                if (child.GetComponent<ItemData>() != null)
                    return child.gameObject;
            }
            return null;
        }
    }

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