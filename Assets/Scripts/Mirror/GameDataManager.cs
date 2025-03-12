using System;
using UnityEngine;
using Zenject;
using static EventsProvider;

public class GameDataManager : IInitializable, IDisposable
{
    public string IP;
    public string Port;
    public bool IsHost;

    private IEventAggregator _eventAggregator;

    [Inject]
    public void Construct(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public void Initialize()
    {
        _eventAggregator.Subscribe<MultiplayerConnectionData>(SetGameData);
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<MultiplayerConnectionData>(SetGameData);
    }

    private void SetGameData(MultiplayerConnectionData eventData) 
    {
        IP = eventData.Ip;
        Port = eventData.Port;
        IsHost = eventData.IsHost;

        Debug.Log($"Set Data: Ip - {IP}, Port - {Port}, IsHost - {IsHost}");
    }

}
