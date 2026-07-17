using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance { get; set; }

    public GameConfig config;

    private float lastAttackTime = 0f;
    private Camera cam;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryAttack();
    }

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

    public float GetDamage() => config.attackDamage;
}