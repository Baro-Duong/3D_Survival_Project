using UnityEngine;

// Rotates the camera based on mouse input (first-person look)
public class MouseMovement : MonoBehaviour
{
    public GameConfig config;

    float xRotation = 0f;
    float yRotation = 0f;

    // Locks the cursor to the game window
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Applies mouse delta to pitch/yaw rotation, unless the inventory screen is open
    void Update()
    {
        if (InventorySystem.Instance.isOpen == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * config.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * config.mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}
