using UnityEngine;

// Raycasts from the camera on left-click and damages a rabbit if hit
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
            RabbitHealth rabbit = hit.collider.GetComponentInParent<RabbitHealth>();
            if (rabbit != null)
            {
                rabbit.TakeDamage(config.attackDamage);
                return;
            }
        }
    }

    // Returns the current attack damage from config
    public float GetDamage() => config.attackDamage;
}
