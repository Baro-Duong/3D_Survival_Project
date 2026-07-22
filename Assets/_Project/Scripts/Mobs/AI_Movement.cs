using UnityEngine;

// Rabbit AI: wanders in a random cardinal direction while calm; once hit (aggressive), chases and
// attacks the player instead
public class AI_Movement : MonoBehaviour
{
    public GameConfig config;

    Animator animator;
    RabbitHealth rabbitHealth;
    Transform player;
    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    int walkDirection;
    public bool isWalking;

    float attackCooldownTimer = 0f;

    // Rolls random walk/wait durations, caches references, and starts moving
    void Start()
    {
        animator = GetComponent<Animator>();
        rabbitHealth = GetComponent<RabbitHealth>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        walkTime = Random.Range(config.rabbitWalkTimeMin, config.rabbitWalkTimeMax);
        waitTime = Random.Range(config.rabbitWaitTimeMin, config.rabbitWaitTimeMax);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Chases and attacks the player once aggressive; otherwise wanders as before
    void Update()
    {
        if (rabbitHealth != null && rabbitHealth.isAggressive)
        {
            ChasePlayer();
            return;
        }

        if (isWalking)
        {
            animator.SetBool("isRunning", true);
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0: transform.localRotation = Quaternion.Euler(0f, 0f, 0f); break;
                case 1: transform.localRotation = Quaternion.Euler(0f, 90f, 0f); break;
                case 2: transform.localRotation = Quaternion.Euler(0f, -90f, 0f); break;
                case 3: transform.localRotation = Quaternion.Euler(0f, 180f, 0f); break;
            }

            transform.position += transform.forward * config.rabbitMoveSpeed * Time.deltaTime;

            if (walkCounter <= 0)
            {
                stopPosition = transform.position;
                isWalking = false;
                transform.position = stopPosition;
                animator.SetBool("isRunning", false);
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0) ChooseDirection();
        }
    }

    // Turns to face the player and moves toward them; attacks (with a cooldown) once in range
    private void ChasePlayer()
    {
        if (player == null) return;

        animator.SetBool("isRunning", true);

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;
        float distance = toPlayer.magnitude;

        if (distance > config.rabbitAttackRange)
        {
            transform.rotation = Quaternion.LookRotation(toPlayer.normalized);
            transform.position += transform.forward * config.rabbitChaseSpeed * Time.deltaTime;
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
            if (attackCooldownTimer <= 0f)
            {
                if (PlayerStats.Instance != null)
                    PlayerStats.Instance.TakeDamage(config.rabbitAttackDamage);
                attackCooldownTimer = config.rabbitAttackCooldown;
            }
        }
    }

    // Picks a new random cardinal direction (0-3) and starts walking
    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    // On touching water, turns 180 degrees and walks away from it
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Water")) return;

        switch (walkDirection)
        {
            case 0: walkDirection = 3; break;
            case 1: walkDirection = 2; break;
            case 2: walkDirection = 1; break;
            case 3: walkDirection = 0; break;
        }

        walkCounter = walkTime;
        isWalking = true;
    }
}
