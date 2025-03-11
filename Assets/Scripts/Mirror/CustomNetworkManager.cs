using UnityEngine;
using Mirror;
using kcp2k;

public class CustomNetworkManager : NetworkManager
{
    public string serverIP = "26.16.112.127"; // Введи свой IP
    public ushort serverPort = 5555; // Порт сервера

    public override void Awake()
    {
        base.Awake();

        // Устанавливаем порт и IP-адрес для KCP Transport
        KcpTransport transport = GetComponent<KcpTransport>();
        if (transport != null)
        {
            transport.Port = serverPort;
            Debug.Log($"✅ Установлен порт: {serverPort}");
        }
        else
        {
            Debug.LogError("❌ KCP Transport не найден!");
        }

        // Устанавливаем IP сервера
        networkAddress = serverIP;
        Debug.Log($"🌐 IP-адрес сервера установлен: {networkAddress}");
    }

    void Start()
    {
        Debug.Log("🚀 Автозапуск сервера...");
        StartHost();
    }

    public override void OnStartServer()
    {
        Debug.Log("✅ Сервер успешно запущен и ждет клиентов...");
        Debug.Log($"🌍 Сервер слушает на: {networkAddress}:{GetComponent<KcpTransport>().Port}");
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log($"🔗 Клиент подключился: {conn.address}");
    }

    public override void OnClientConnect()
    {
        Debug.Log("✅ Клиент успешно подключился к серверу!");
    }
}
