using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private float _fireRate = 5f;
    [SerializeField] private float _fireRange = 100f;
    [SerializeField] private Vector3 _recoilOffset = new Vector3(0f, -1f, 0f);
    [SerializeField] private float _multiplier = 100f;
    [SerializeField] private float _recoilReturnSpeed = 10f;

    [SerializeField] private Transform _firePoint;

    private float _nextFireTime = 0f;
    private Vector3 _originalPosition;
    private Vector3 _currentRecoilOffset;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        else if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            Fire();
            _nextFireTime = Time.time + 1f / _fireRate;
        }

        _currentRecoilOffset = Vector3.Lerp(_currentRecoilOffset, Vector3.zero, Time.deltaTime * _recoilReturnSpeed);
        transform.localPosition = _originalPosition + _currentRecoilOffset;
    }

    private void Fire()
    {
        Ray ray = new Ray(_firePoint.position, _firePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _fireRange))
        {
            //Debug.Log($"Попали в: {hit.collider.name}");
        }

        _currentRecoilOffset += _recoilOffset/ _multiplier;
    }
}
