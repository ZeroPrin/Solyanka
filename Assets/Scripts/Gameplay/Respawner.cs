using UnityEngine;
using Mirror;
using Zenject;
using System.Collections.Generic;
using static EventsProvider;

public class Respawner : NetworkBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;

    private IEventAggregator _eventAggregator;

    [Inject]
    public void Construct(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public override void OnStartServer()
    {
        _eventAggregator.Subscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    public override void OnStopServer()
    {
        _eventAggregator.Unsubscribe<PlayerDiedEvent>(OnPlayerDied);
    }

    [Server]
    private void OnPlayerDied(PlayerDiedEvent eventData)
    {
        GameObject deadPlayer = eventData.GameObject;
        if (deadPlayer == null) return;

        Transform spawnPoint = GetRandomSpawnPoint();

        Health health = deadPlayer.GetComponent<Health>();
        if (health != null)
        {
            health.RestoreFullHealth();
        }

        NetworkIdentity identity = deadPlayer.GetComponent<NetworkIdentity>();
        if (identity != null && identity.connectionToClient != null)
        {
            TargetRespawn(identity.connectionToClient, spawnPoint.position, spawnPoint.rotation);
        }
    }

    [TargetRpc]
    private void TargetRespawn(NetworkConnection target, Vector3 pos, Quaternion rot)
    {
        GameObject localPlayer = NetworkClient.localPlayer?.gameObject;
        if (localPlayer == null) return;

        localPlayer.transform.SetPositionAndRotation(pos, rot);
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];
    }
}
