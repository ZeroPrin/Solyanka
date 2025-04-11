using Mirror;
using UnityEngine;
using Zenject;

public class CustomNetworkManager : NetworkManager
{
    private GameDataManager _gameDataManager;
    private DiContainer _container;

    [Inject]
    public void Construct(GameDataManager gameDataManager, DiContainer container)
    {
        _gameDataManager = gameDataManager;
        _container = container;
    }

    public override void Start()
    {
        base.Start();

        if (_gameDataManager.IsHost)
        {
            Debug.Log("[NetworkManager] Запуск сервера");
            StartHost();
        }
        else
        {
            Debug.Log($"[NetworkManager] Подключаемся к серверу: {_gameDataManager.IP}:{_gameDataManager.Port}");
            networkAddress = _gameDataManager.IP;
            StartClient(); 
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPosition = GetStartPosition();

        Vector3 spawnPosition = startPosition != null ? startPosition.position : Vector3.zero;
        Quaternion spawnRotation = startPosition != null ? startPosition.rotation : Quaternion.identity;

        GameObject player = _container.InstantiatePrefab(playerPrefab, spawnPosition, spawnRotation, null);

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    [Server]
    public void SpawnPlayer(NetworkConnectionToClient conn, Vector3 position)
    {
        GameObject player = _container.InstantiatePrefab(playerPrefab, position, Quaternion.identity, null);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (conn.identity != null)
        {
            NetworkServer.Destroy(conn.identity.gameObject);
            Debug.Log($"[SERVER] Игрок {conn.connectionId} отключился");
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log($"[SERVER] Новый игрок подключился: {conn.connectionId}");
    }
}
