using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        UpdateHighlight();
    }

    private void Update()
    {
        HandleNumberKeys();
        HandleScrollWheel();

        if (Input.GetKeyDown(KeyCode.Q)) DropSelectedItem();
        if (Input.GetKeyDown(KeyCode.F)) ConsumeSelectedItem();
    }

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

    private void HandleScrollWheel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) { selectedIndex = (selectedIndex - 1 + 8) % 8; UpdateHighlight(); }
        else if (scroll < 0f) { selectedIndex = (selectedIndex + 1) % 8; UpdateHighlight(); }
    }

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

    private void ConsumeSelectedItem()
    {
        if (hotbarSlots[selectedIndex] == null) return;
        ItemSlot slot = hotbarSlots[selectedIndex].GetComponent<ItemSlot>();
        if (slot == null || slot.Item == null) return;

        GameObject item = slot.Item;
        ItemData data = item.GetComponent<ItemData>();
        if (data == null || !data.isConsumable) return;

        string itemName = data.itemName;

        // Hồi stats
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.EatFood(data.hungerRestore);
            PlayerStats.Instance.DrinkWater(data.thirstRestore);
        }

        // Xóa item cũ
        item.transform.SetParent(null);
        item.SetActive(false);
        Destroy(item);
        InventorySystem.Instance.itemList.Remove(itemName);

        // Nếu là WaterBottle → spawn Bottle rỗng vào đúng slot
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

    public GameObject GetSelectedItem()
    {
        if (hotbarSlots[selectedIndex] == null) return null;
        ItemSlot slot = hotbarSlots[selectedIndex].GetComponent<ItemSlot>();
        return slot?.Item;
    }
}