using UnityEngine;
using Mirror;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        if (!isLocalPlayer)
        {
            _playerCamera.gameObject.SetActive(false);
            enabled = false;
        }
        else
        {
            _playerCamera.gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        FollowHead();
    }

    private void FollowHead()
    {
        if (_headTransform == null) return;

        _playerCamera.transform.position = _headTransform.position + _cameraOffset;
        _playerCamera.transform.LookAt(_headTransform.position);
    }
}
