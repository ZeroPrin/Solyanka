using Mirror;
using UnityEngine;

public class PlayerMirror : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public Transform cameraTransform;
    public Transform rayOrigin;
    public float rayDistance = 10f;

    private float rotationX = 0f;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableCameraForOtherPlayers();
        }
    }

    public override void OnStartAuthority()
    {
        EnableCameraForLocalPlayer();
    }

    void EnableCameraForLocalPlayer()
    {
        if (cameraTransform != null)
        {
            cameraTransform.gameObject.SetActive(true);
            Debug.Log($"[Client] Камера активирована для {netId}");
        }
    }

    void DisableCameraForOtherPlayers()
    {
        if (cameraTransform != null)
        {
            cameraTransform.gameObject.SetActive(false);
            Debug.Log($"[Client] Камера отключена для {netId}");
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        HandleMovement();
        HandleLook();

        if (Input.GetMouseButtonDown(1))
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
            if (hit.collider.CompareTag("Player"))
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
