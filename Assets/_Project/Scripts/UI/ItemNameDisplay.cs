using TMPro;
using UnityEngine;

public class ItemNameDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    void Update()
    {
        GameObject target = InventorySystem.Instance.isOpen
            ? ItemSlot.hoveredItem
            : HotbarSelection.Instance.GetSelectedItem();

        nameText.text = target != null ? GetItemName(target) : "";
    }

    private string GetItemName(GameObject item)
    {
        ItemData data = item.GetComponent<ItemData>();
        return data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();
    }
}