using UnityEngine;

public class CameraFollowHead : MonoBehaviour
{
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 0f, 0f);

    private void Update()
    {
        FollowHead();
    }

    private void FollowHead()
    {
        transform.position = _headTransform.position + _cameraOffset;

        transform.LookAt(_headTransform.position);
    }
}
