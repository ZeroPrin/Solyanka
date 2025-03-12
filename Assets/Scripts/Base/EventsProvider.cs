
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;

public static class EventsProvider
{
    public class MultiplayerConnectionData
    {
        public readonly string Ip;
        public readonly string Port;
        public readonly bool IsHost;

        public MultiplayerConnectionData(string ip, string port, bool isHost)
        {
            Ip = ip;
            Port = port;
            IsHost = isHost;
        }
    }
}
