using Mirror;
using Mirror.Examples.Common.Controllers.Player;
using UnityEngine;


public class PlayerMirror : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public Transform cameraTransform;
    public Transform rayOrigin; // Точка, из которой выпускаем луч
    public float rayDistance = 10f; // Дальность выстрела

    private float rotationX = 0f;

    void Start()
    {
        if (!isLocalPlayer)
        {
            // Отключаем управление камерой для других игроков
            cameraTransform.gameObject.SetActive(false);
            return;
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return; // Только локальный игрок управляет

        HandleMovement();
        HandleLook();

        if (Input.GetMouseButtonDown(1)) // ПКМ - стрельба лучом
        {
            CmdShoot();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    [Command]
    void CmdShoot()
    {
        Debug.Log("Игрок " + netId + " выстрелил!");

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Player")) // Проверяем, попали ли в игрока
            {
                hit.collider.GetComponent<PlayerMirror>().RpcOnHit();
            }
        }
    }

    [ClientRpc]
    void RpcOnHit()
    {
        Debug.Log("Игрок " + netId + " получил попадание!");
    }
}
