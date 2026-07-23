using UnityEngine;

// Raycasts from the camera on left-click; damages a rabbit, chops a tree if holding an Axe, or hits a bush (any item)
public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance { get; set; }

    public GameConfig config;

    private float lastAttackTime = 0f;
    private Camera cam;

    // Singleton setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Caches the main camera
    private void Start()
    {
        cam = Camera.main;
    }

    // Listens for the attack input
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryAttack();
    }

    // Raycasts from screen center and applies damage to a RabbitHealth if hit, respecting the attack cooldown
    private void TryAttack()
    {
        if (Time.time - lastAttackTime < config.attackCooldown) return;
        lastAttackTime = Time.time;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, config.attackRange))
        {
            bool holdingAxe = GetHeldItemName() == "Axe";

            RabbitHealth rabbit = hit.collider.GetComponentInParent<RabbitHealth>();
            if (rabbit != null)
            {
                rabbit.TakeDamage(holdingAxe ? config.axeAttackDamage : config.attackDamage);
                if (holdingAxe) ConsumeAxeDurability();
                return;
            }

            Tree tree = hit.collider.GetComponentInParent<Tree>();
            if (tree != null && holdingAxe)
            {
                tree.Chop();
                ConsumeAxeDurability();
                return;
            }

            Bush bush = hit.collider.GetComponentInParent<Bush>();
            if (bush != null)
            {
                bush.TryHarvest();
                return;
            }
        }
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

    // Consumes 1 use from the currently held Axe's durability, breaking (destroying) it once it hits 0
    private void ConsumeAxeDurability()
    {
        GameObject item = HotbarSelection.Instance.GetSelectedItem();
        if (item == null) return;

        ItemData data = item.GetComponent<ItemData>();
        if (data == null || data.maxDurability <= 0) return;

        data.currentDurability--;

        ItemSlot slot = item.GetComponentInParent<ItemSlot>();

        if (data.currentDurability <= 0)
        {
            InventorySystem.Instance.itemList.Remove(data.itemName);
            item.transform.SetParent(null);
            Destroy(item);
        }

        if (slot != null) slot.RefreshStackDisplay();
    }

    // Returns the current bare-hand attack damage from config
    public float GetDamage() => config.attackDamage;
}
