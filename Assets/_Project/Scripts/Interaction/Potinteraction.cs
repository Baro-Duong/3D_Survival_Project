using UnityEngine;

// Gắn vào Player
public class PotInteraction : MonoBehaviour
{
    [Header("References")]
    public GameConfig config;
    public Transform playerCamera;
    public float interactRange = 20f; // đồng bộ với SelectionManager.interactRange

    // Cooking
    private float cookHoldTime = 0f;
    private float cookRequiredTime = 10f;
    private bool isCooking = false;

    private void Update()
    {
        if (InventorySystem.Instance.isOpen) return;

        string heldItem = GetHeldItemName();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactRange);

        HandleInteractionText(heldItem, hasHit, hit);
        HandleInput(heldItem, hasHit, hit);
    }

    private void HandleInteractionText(string heldItem, bool hasHit, RaycastHit hit)
    {
        if (!hasHit) { HideText(); return; }

        string hitTag = hit.collider.tag;

        if (heldItem == "Pot" && hitTag == "Water")
            ShowText("Take Dirty Water");
        else if (heldItem == "DirtyWaterPot" && hitTag == "FirePit")
            ShowText("Boil Water");
        else if (heldItem == "Bottle" && hit.collider.GetComponent<FirePitManager>() != null
            && hit.collider.GetComponent<FirePitManager>().state == FirePitManager.FirePitState.BoiledWater)
            ShowText("Scoop Water");
        else if (heldItem == "RawMeat" && hitTag == "FirePit")
        {
            float pct = isCooking ? cookHoldTime / cookRequiredTime * 100f : 0f;
            ShowText(isCooking ? $"Cooking... {(int)pct}%" : "Hold F to Cook Meat");
        }
        else
            HideText();
    }

    private void HandleInput(string heldItem, bool hasHit, RaycastHit hit)
    {
        if (!hasHit) return;

        string hitTag = hit.collider.tag;

        // Pot + Water → DirtyWaterPot
        if (heldItem == "Pot" && hitTag == "Water" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ReplaceHeldItem("Pot", "DirtyWaterPot");
            return;
        }

        // DirtyWaterPot + FirePit → bắt đầu boil
        if (heldItem == "DirtyWaterPot" && hitTag == "FirePit" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.state == FirePitManager.FirePitState.Normal)
            {
                RemoveHeldItem("DirtyWaterPot");
                fp.StartBoiling();
            }
            return;
        }

        // Bottle + BoiledWaterFirePit → WaterBottle
        if (heldItem == "Bottle" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.state == FirePitManager.FirePitState.BoiledWater)
            {
                ReplaceHeldItem("Bottle", "WaterBottle");
                fp.ScoopWater();
            }
            return;
        }

        // RawMeat + FirePit → CookedMeat (giữ F 10s)
        if (heldItem == "RawMeat" && hitTag == "FirePit")
        {
            if (Input.GetKey(KeyCode.F))
            {
                isCooking = true;
                cookHoldTime += Time.deltaTime;
                if (cookHoldTime >= cookRequiredTime)
                {
                    ReplaceHeldItem("RawMeat", "CookedMeat");
                    isCooking = false;
                    cookHoldTime = 0f;
                }
            }
            else
            {
                isCooking = false;
                cookHoldTime = 0f;
            }
            return;
        }

        // Reset cooking nếu không nhìn vào FirePit
        isCooking = false;
        cookHoldTime = 0f;
    }

    private string GetHeldItemName()
    {
        if (HotbarSelection.Instance == null) return "";
        GameObject item = HotbarSelection.Instance.GetSelectedItem();
        if (item == null) return "";
        ItemData data = item.GetComponent<ItemData>();
        return data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();
    }

    private void ReplaceHeldItem(string oldName, string newName)
    {
        int index = HotbarSelection.Instance.selectedIndex;
        GameObject[] slots = HotbarSelection.Instance.hotbarSlots;
        if (slots[index] == null) return;

        ItemSlot slot = slots[index].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        // Xóa item cũ
        slot.Item.transform.SetParent(null);
        Destroy(slot.Item);
        InventorySystem.Instance.itemList.Remove(oldName);

        // Spawn item mới vào slot
        GameObject prefab = Resources.Load<GameObject>(newName);
        if (prefab == null) { Debug.LogError("Không tìm thấy prefab: " + newName); return; }

        GameObject newItem = Instantiate(prefab, slots[index].transform.position, Quaternion.identity);
        newItem.transform.SetParent(slots[index].transform);
        newItem.transform.localPosition = Vector2.zero;
        InventorySystem.Instance.itemList.Add(newName);

        slot.RefreshStackDisplay();
    }

    private void RemoveHeldItem(string itemName)
    {
        int index = HotbarSelection.Instance.selectedIndex;
        GameObject[] slots = HotbarSelection.Instance.hotbarSlots;
        if (slots[index] == null) return;

        ItemSlot slot = slots[index].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        slot.Item.transform.SetParent(null);
        Destroy(slot.Item);
        InventorySystem.Instance.itemList.Remove(itemName);
        slot.RefreshStackDisplay();
    }

    private void ShowText(string text)
    {
        if (SelectionManager.Instance != null)
        {
            SelectionManager.Instance.overrideText = true;
            SelectionManager.Instance.interaction_Info_UI.SetActive(true);
            SelectionManager.Instance.interaction_text.text = text;
        }
    }

    private void HideText()
    {
        if (SelectionManager.Instance != null)
        {
            // Không tự SetActive(false) ở đây — SelectionManager mới là chủ sở hữu
            // của interaction_Info_UI, để nó tự quyết định active/inactive dựa trên
            // raycast của chính nó ở frame kế tiếp. Nếu gọi SetActive(false) tại đây,
            // nó có thể ghi đè lên SetActive(true) mà SelectionManager vừa gọi trong
            // cùng frame (thứ tự Update() giữa 2 script không được đảm bảo).
            SelectionManager.Instance.overrideText = false;
        }
    }
}