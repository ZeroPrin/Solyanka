using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character stats")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;

    private void Move()
    {
        
    }
}
