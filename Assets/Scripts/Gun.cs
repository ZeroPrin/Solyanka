using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected int _bulletCount;

    public abstract void Shoot();
}
