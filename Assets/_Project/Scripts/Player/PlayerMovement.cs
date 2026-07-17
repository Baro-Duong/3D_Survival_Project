using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameConfig config;

    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public bool isSprinting { get; private set; }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, config.groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0;
        float currentSpeed = isSprinting ? config.sprintSpeed : config.walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(config.jumpHeight * -2f * config.gravity);

        velocity.y += config.gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}