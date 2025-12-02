using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject mainCamera;

    public float speed = 5f; // Скорость движения
    public float jumpForce = 5f; // Сила прыжка
    public float mouseSensitivity = 100f; // Чувствительность мыши
    public float verticalRotationLimit = 80f; // Ограничение вертикального вращения

    private Rigidbody rb;
    private bool isGrounded;
    
    private float rotationX = 0f; // Вертикальный угол
    private float rotationY = 0f; // Горизонтальный угол

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Закрепление курсора в центре экрана
    }

    void Update()
    {
        MovePlayer();
        Jump();
        RotateCamera();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = mainCamera.transform.TransformDirection(movement); // Относительно направления камеры
        movement.y = 0; // Убираем вертикальное движение

        transform.Translate(movement * speed * Time.deltaTime, Space.World); // Перемещение с использованием Transform.Translate
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalRotationLimit, verticalRotationLimit);
        rotationY += mouseX;

        mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Вращение камеры
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // Вращение персонажа
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
