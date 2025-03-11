using Mirror;
using UnityEngine;

public class PlayerMirror : NetworkBehaviour
{
    public Transform cameraTransform; // Ссылка на камеру (должна быть дочерним объектом)
    public float mouseSensitivity = 200f; // Чувствительность мыши
    public float movementSpeed = 5f; // Скорость передвижения

    private float verticalRotation = 0f; // Угол наклона камеры (ограничение)

    void Start()
    {
        if (isOwned)
        {
            Cursor.lockState = CursorLockMode.Locked; // Прячем курсор
        }
    }

    void Update()
    {
        if (!isOwned) return; // Проверяем, управляем ли мы этим объектом

        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // Движение по X
        float v = Input.GetAxis("Vertical");   // Движение по Z

        Vector3 movement = transform.right * h + transform.forward * v; // Двигаем в сторону взгляда
        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Поворот игрока (по горизонтали)
        transform.Rotate(Vector3.up * mouseX);

        // Поворот камеры (по вертикали)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Ограничение от -90° до 90°
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
