using UnityEngine;
using Mirror;

public class Rifle : NetworkBehaviour
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
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryFire();
        }
        else if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            TryFire();
            _nextFireTime = Time.time + 1f / _fireRate;
        }

        _currentRecoilOffset = Vector3.Lerp(_currentRecoilOffset, Vector3.zero, Time.deltaTime * _recoilReturnSpeed);
        transform.localPosition = _originalPosition + _currentRecoilOffset;
    }

    private void TryFire()
    {
        Vector3 fireDirection = _firePoint.forward;
        CmdFire(fireDirection);
        _currentRecoilOffset += _recoilOffset / _multiplier;
    }

    [Command]
    private void CmdFire(Vector3 direction)
    {
        Ray ray = new Ray(_firePoint.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, _fireRange))
        {
            Debug.Log($"[Server] Попали в: {hit.collider.name}");

            var health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                health.ApplyDamage(20);
            }
        }

        RpcFireEffects();
    }


    [ClientRpc]
    private void RpcFireEffects()
    {

    }
}
