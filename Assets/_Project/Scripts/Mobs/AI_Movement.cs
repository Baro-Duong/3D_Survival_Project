using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    public GameConfig config;

    Animator animator;
    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    int walkDirection;
    public bool isWalking;

    void Start()
    {
        animator = GetComponent<Animator>();

        walkTime = Random.Range(config.rabbitWalkTimeMin, config.rabbitWalkTimeMax);
        waitTime = Random.Range(config.rabbitWaitTimeMin, config.rabbitWaitTimeMax);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    void Update()
    {
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

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Water")) return;

        // Quay đầu (180 độ) và đi ra xa khỏi nước
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