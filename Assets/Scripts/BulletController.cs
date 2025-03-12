using UnityEngine;

public class BulletController : MonoBehaviour
{
    private ObjectPool<BulletController> _pool;
    private float _speed = 20f;
    private float _lifetime = 3f;

    public void Initialize(ObjectPool<BulletController> bulletPool)
    {
        _pool = bulletPool;
    }
    
    private void OnEnable()
    {
        //Invoke(nameof(Deactivate), _lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void Deactivate()
    {
        _pool.ReturnToPool(this);
    }
}