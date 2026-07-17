using UnityEngine;
using UnityEngine.UI;

// Handles hotbar slot selection (keys/scroll), the highlight, and dropping/consuming the selected item
public class HotbarSelection : MonoBehaviour
{
    public static HotbarSelection Instance { get; set; }

    public GameConfig config;

    [Header("Hotbar Slots")]
    public GameObject[] hotbarSlots = new GameObject[8];

    [Header("Highlight")]
    public Color normalColor = new Color(1f, 1f, 1f, 0.3f);
    public Color selectedColor = new Color(1f, 1f, 0f, 0.8f);

    public int selectedIndex { get; private set; } = 0;

    public Transform playerCamera;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Shows the initial highlight
    private void Start()
    {
        UpdateHighlight();
    }

    // Reads selection input each frame plus the drop (Q) and consume (F) keys
    private void Update()
    {
        HandleNumberKeys();
        HandleScrollWheel();

        if (Input.GetKeyDown(KeyCode.Q)) DropSelectedItem();
        if (Input.GetKeyDown(KeyCode.F)) ConsumeSelectedItem();
    }

    // Selects a slot via the 1-8 number keys
    private void HandleNumberKeys()
    {
        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                UpdateHighlight();
                return;
            }
        }
    }

    // Cycles the selected slot via the mouse scroll wheel
    private void HandleScrollWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) { selectedIndex = (selectedIndex - 1 + 8) % 8; UpdateHighlight(); }
        else if (scroll < 0f) { selectedIndex = (selectedIndex + 1) % 8; UpdateHighlight(); }
    }

    // Recolors every slot so only the selected one is highlighted
    private void UpdateHighlight()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i] == null) continue;
            Image img = hotbarSlots[i].GetComponent<Image>();
            if (img != null)
                img.color = (i == selectedIndex) ? selectedColor : normalColor;
        }
    }

    // Removes one item from the selected slot (or the whole stack if only 1 left) and spawns its world prefab in front of the player
    private void DropSelectedItem()
    {
        if (hotbarSlots[selectedIndex] == null) return;
        ItemSlot slot = hotbarSlots[selectedIndex].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        GameObject item = slot.Item;
        ItemData data = item.GetComponent<ItemData>();
        string itemName = data != null ? data.itemName : item.name.Replace("(Clone)", "").Trim();

        if (data != null && data.currentStack > 1)
        {
            data.currentStack--;
            slot.RefreshStackDisplay();
        }
        else
        {
            item.transform.SetParent(null);
            item.SetActive(false);
            Destroy(item);
            slot.RefreshStackDisplay();
        }

        InventorySystem.Instance.itemList.Remove(itemName);

        GameObject worldPrefab = Resources.Load<GameObject>("WorldItems/" + itemName);
        if (worldPrefab == null) worldPrefab = Resources.Load<GameObject>(itemName);

        if (worldPrefab != null && playerCamera != null)
        {
            Vector3 spawnPos = playerCamera.position + playerCamera.forward * 1.5f;
            GameObject dropped = Instantiate(worldPrefab, spawnPos, Quaternion.identity);
            Rigidbody rb = dropped.GetComponent<Rigidbody>();
            if (rb == null) rb = dropped.AddComponent<Rigidbody>();
            rb.AddForce(playerCamera.forward * config.dropForce, ForceMode.Impulse);
        }
    }

    // Consumes the selected item (restoring hunger/thirst) and destroys it; WaterBottle leaves behind an empty Bottle
    private void ConsumeSelectedItem()
    {
        if (hotbarSlots[selectedIndex] == null) return;
        ItemSlot slot = hotbarSlots[selectedIndex].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        GameObject item = slot.Item;
        ItemData data = item.GetComponent<ItemData>();
        if (data == null || !data.isConsumable) return;

        string itemName = data.itemName;

        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.EatFood(data.hungerRestore);
            PlayerStats.Instance.DrinkWater(data.thirstRestore);
        }

        item.transform.SetParent(null);
        item.SetActive(false);
        Destroy(item);
        InventorySystem.Instance.itemList.Remove(itemName);

        if (itemName == "WaterBottle")
        {
            GameObject bottlePrefab = Resources.Load<GameObject>("Bottle");
            if (bottlePrefab != null)
            {
                GameObject bottle = Instantiate(bottlePrefab, hotbarSlots[selectedIndex].transform.position, Quaternion.identity);
                bottle.transform.SetParent(hotbarSlots[selectedIndex].transform);
                bottle.transform.localPosition = Vector2.zero;
                InventorySystem.Instance.itemList.Add("Bottle");
            }
        }

        slot.RefreshStackDisplay();
    }

    // Returns the item GameObject currently equipped in the selected slot, or null
    public GameObject GetSelectedItem()
    {
        if (hotbarSlots[selectedIndex] == null) return null;
        ItemSlot slot = hotbarSlots[selectedIndex].GetComponent<ItemSlot>();
        return slot?.Item;
    }
}
