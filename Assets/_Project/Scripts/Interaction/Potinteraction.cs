using UnityEngine;

// Attached to the Player; drives the Pot/Water/FirePit cooking chain (fill, boil, scoop, cook)
public class PotInteraction : MonoBehaviour
{
    [Header("References")]
    public GameConfig config;
    public Transform playerCamera;
    public float interactRange = 20f; // kept in sync with SelectionManager.interactRange

    // Cooking
    private float cookHoldTime = 0f;
    private bool isCooking = false;

    // Raycasts forward each frame and updates both the interaction text and the input handling
    private void Update()
    {
        if (InventorySystem.Instance.isOpen) return;

        string heldItem = GetHeldItemName();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactRange);

        HandleInteractionText(heldItem, hasHit, hit);
        HandleInput(heldItem, hasHit, hit);
    }

    // Shows the matching prompt text ("Take Dirty Water", "Boil Water", ...) for the held item + target combo
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
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.uses <= 0)
                ShowText("FirePit is worn out");
            else
            {
                float pct = isCooking ? cookHoldTime / config.cookRequiredTime * 100f : 0f;
                ShowText(isCooking ? $"Cooking... {(int)pct}%" : "Hold F to Cook Meat");
            }
        }
        else if (heldItem == "Stick" && hitTag == "FirePit")
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.uses < config.firePitMaxUses)
                ShowText($"Add Stick (+{config.stickRepairUses} Uses)");
            else
                ShowText("FirePit is already full");
        }
        else if (heldItem == "Rock" && hitTag == "FirePit")
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.uses < config.firePitMaxUses)
                ShowText($"Add Rock (+{config.rockRepairUses} Uses)");
            else
                ShowText("FirePit is already full");
        }
        else if (hitTag == "FirePit" && hit.collider.GetComponent<FirePitManager>() != null)
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            ShowText($"Uses: {fp.uses}/{config.firePitMaxUses}");
        }
        else
            HideText();
    }

    // Runs the actual click/hold actions for each step of the cooking chain
    private void HandleInput(string heldItem, bool hasHit, RaycastHit hit)
    {
        if (!hasHit) { isCooking = false; cookHoldTime = 0f; return; }

        string hitTag = hit.collider.tag;

        // Pot + Water -> DirtyWaterPot
        if (heldItem == "Pot" && hitTag == "Water" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ReplaceHeldItem("Pot", "DirtyWaterPot");
            return;
        }

        // DirtyWaterPot + FirePit -> starts boiling
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

        // Bottle + BoiledWaterFirePit -> WaterBottle
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

        // Stick + FirePit -> restores uses (sink for surplus Stick from Bush), consumes 1 Stick
        if (heldItem == "Stick" && hitTag == "FirePit" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.uses < config.firePitMaxUses)
            {
                fp.AddUses(config.stickRepairUses);
                ConsumeOneFromHeld();
            }
            return;
        }

        // Rock + FirePit -> restores uses, consumes 1 Rock
        if (heldItem == "Rock" && hitTag == "FirePit" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp != null && fp.uses < config.firePitMaxUses)
            {
                fp.AddUses(config.rockRepairUses);
                ConsumeOneFromHeld();
            }
            return;
        }

        // RawMeat + FirePit -> CookedMeat (hold F for 10s), blocked once the FirePit is worn out
        if (heldItem == "RawMeat" && hitTag == "FirePit")
        {
            FirePitManager fp = hit.collider.GetComponent<FirePitManager>();
            if (fp == null || fp.uses <= 0)
            {
                isCooking = false;
                cookHoldTime = 0f;
                return;
            }

            if (Input.GetKey(KeyCode.F))
            {
                isCooking = true;
                cookHoldTime += Time.deltaTime;
                if (cookHoldTime >= config.cookRequiredTime)
                {
                    ConsumeOneAndAdd("CookedMeat");
                    fp.ConsumeCookUse();
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

        // Not looking at a FirePit: reset the cooking hold progress
        isCooking = false;
        cookHoldTime = 0f;
    }

    // Returns the name of the item currently equipped on the hotbar, or "" if none
    private string GetHeldItemName()
    {
        if (HotbarSelection.Instance == null) return "";
        GameObject item = HotbarSelection.Instance.GetSelectedItem();
        if (item == null) return "";
        ItemData data = item.GetComponent<ItemData>();
        return data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();
    }

    // Destroys the held item and spawns a different one into the same hotbar slot
    private void ReplaceHeldItem(string oldName, string newName)
    {
        int index = HotbarSelection.Instance.selectedIndex;
        GameObject[] slots = HotbarSelection.Instance.hotbarSlots;
        if (slots[index] == null) return;

        ItemSlot slot = slots[index].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        slot.Item.transform.SetParent(null);
        Destroy(slot.Item);
        InventorySystem.Instance.itemList.Remove(oldName);

        GameObject prefab = Resources.Load<GameObject>(newName);
        if (prefab == null) { Debug.LogError("Prefab not found: " + newName); return; }

        GameObject newItem = Instantiate(prefab, slots[index].transform.position, Quaternion.identity);
        newItem.transform.SetParent(slots[index].transform);
        newItem.transform.localPosition = Vector2.zero;
        InventorySystem.Instance.itemList.Add(newName);

        slot.RefreshStackDisplay();
    }

    // Consumes 1 unit from the held item's stack (destroying it only if that was the last one), then adds
    // 1 of a different item to inventory — used for cooking, where the source item can be a stack (RawMeat)
    // but ReplaceHeldItem would wrongly destroy the whole stack for a single cook
    private void ConsumeOneAndAdd(string newName)
    {
        int index = HotbarSelection.Instance.selectedIndex;
        GameObject[] slots = HotbarSelection.Instance.hotbarSlots;
        if (slots[index] == null) return;

        ItemSlot slot = slots[index].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        ItemData data = slot.Item.GetComponent<ItemData>();
        string oldName = data != null ? data.itemName : slot.Item.name.Replace("(Clone)", "").Trim();

        if (data != null && data.currentStack > 1)
        {
            data.currentStack--;
        }
        else
        {
            slot.Item.transform.SetParent(null);
            Destroy(slot.Item);
        }
        slot.RefreshStackDisplay();

        InventorySystem.Instance.itemList.Remove(oldName);

        InventorySystem.Instance.AddToInvetory(newName);
    }

    // Consumes 1 unit from the held item's stack (destroying it only if that was the last one), with no
    // replacement item produced — used when an item is spent as a pure resource, e.g. Stick as FirePit fuel
    private void ConsumeOneFromHeld()
    {
        int index = HotbarSelection.Instance.selectedIndex;
        GameObject[] slots = HotbarSelection.Instance.hotbarSlots;
        if (slots[index] == null) return;

        ItemSlot slot = slots[index].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        ItemData data = slot.Item.GetComponent<ItemData>();
        string itemName = data != null ? data.itemName : slot.Item.name.Replace("(Clone)", "").Trim();

        if (data != null && data.currentStack > 1)
        {
            data.currentStack--;
        }
        else
        {
            slot.Item.transform.SetParent(null);
            Destroy(slot.Item);
        }
        slot.RefreshStackDisplay();

        InventorySystem.Instance.itemList.Remove(itemName);
    }

    // Destroys the held item without replacing it (consumed by the action, e.g. DirtyWaterPot into the fire)
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

    // Displays the given prompt text via SelectionManager and marks it as overridden
    private void ShowText(string text)
    {
        if (SelectionManager.Instance != null)
        {
            SelectionManager.Instance.overrideText = true;
            SelectionManager.Instance.interaction_Info_UI.SetActive(true);
            SelectionManager.Instance.interaction_text.text = text;
        }
    }

    // Clears the override flag so SelectionManager can decide the text/active-state itself next frame
    private void HideText()
    {
        if (SelectionManager.Instance != null)
        {
            // Deliberately not calling SetActive(false) here — SelectionManager owns interaction_Info_UI
            // and decides active/inactive itself next frame based on its own raycast. Calling
            // SetActive(false) here could stomp a SetActive(true) SelectionManager just made in the same
            // frame, since the Update() order between the two scripts isn't guaranteed.
            SelectionManager.Instance.overrideText = false;
        }
    }
}
