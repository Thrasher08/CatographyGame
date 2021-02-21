using Cinemachine;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float movementSpeed = 5f;
    public float gravity = -9.81f;

    public Transform player;
    public CharacterController playerController;

    private float xRotation = 0f;

    private Vector3 velocity;

    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

        Vector3 newPosition = transform.right * x + transform.forward * z;

        playerController.Move(newPosition * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);
    }

}
