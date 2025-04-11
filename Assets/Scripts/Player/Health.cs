using UnityEngine;
using Mirror;
using Zenject;
using static EventsProvider;

public class Health : NetworkBehaviour
{
    [SyncVar] public int СurrentHealth;

    [SerializeField] private int _maxHealth = 100;

    private IEventAggregator _eventAggregator;

    [Inject]
    public void Construct(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public override void OnStartServer()
    {
        СurrentHealth = _maxHealth;
    }

    [Server]
    public void ApplyDamage(int amount)
    {
        if (СurrentHealth <= 0) return;

        СurrentHealth -= amount;

        if (СurrentHealth <= 0)
        {
            СurrentHealth = 0;
            _eventAggregator.Publish(new PlayerDiedEvent(gameObject));
        }
    }

    [Server]
    public void RestoreFullHealth()
    {
        СurrentHealth = _maxHealth;
    }
}
