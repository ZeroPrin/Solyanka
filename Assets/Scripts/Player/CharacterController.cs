using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float rotationSpeed = 5f;
    public float distance = 5f;
    public float mouseSensitivity = 2f;

    private float yaw = 0f;
    private float pitch = 0f;

    private void Update()
    {
        HandleRotation();
        UpdateTargetPosition();
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch += mouseY;
    }

    private void UpdateTargetPosition()
    {
        Vector3 offset = new Vector3(
            Mathf.Sin(yaw * Mathf.Deg2Rad) * Mathf.Cos(pitch * Mathf.Deg2Rad),
            Mathf.Sin(pitch * Mathf.Deg2Rad),
            Mathf.Cos(yaw * Mathf.Deg2Rad) * Mathf.Cos(pitch * Mathf.Deg2Rad)
        ) * distance;

        target.position = player.position + offset;
    }
}
