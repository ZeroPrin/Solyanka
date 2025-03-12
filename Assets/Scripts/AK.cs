using UnityEngine;
using Zenject.SpaceFighter;

public class AK : Gun
{
    public BulletController bulletPrefab;
    public Transform firePoint;
    private ObjectPool<BulletController> _bulletPool;
    
    private void Start()
    {
        _bulletPool = new ObjectPool<BulletController>(bulletPrefab, 20);
    }
    
    public override void Shoot()
    {
        var bullet = _bulletPool.Get();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.Initialize(_bulletPool);
    }
}