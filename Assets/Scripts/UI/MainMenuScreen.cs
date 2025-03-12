using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static EventsProvider;
using static Enums;

public class MainMenuScreen : ScreenUI
{
    [SerializeField] private TMP_InputField _ipInputField;
    [SerializeField] private TMP_InputField _portInputField;

    [SerializeField] private Button _hostConnectBtn;
    [SerializeField] private Button _clientConnectBtn;

    private IEventAggregator _eventAggregator;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(IEventAggregator eventAggregator, SceneLoader sceneLoader)
    {
        _eventAggregator = eventAggregator;
        _sceneLoader = sceneLoader;
    }

    public override void Initialize()
    {
        _ipInputField.text = "26.16.112.127";
        _portInputField.text = "5555";

        _hostConnectBtn.onClick.AddListener(HostButtonClick);
        _clientConnectBtn.onClick.AddListener(ClientButtonClick);
    }

    public override void Deinitialize()
    {
        _hostConnectBtn.onClick.RemoveListener(HostButtonClick);
        _clientConnectBtn.onClick.RemoveListener(ClientButtonClick);
    }

    private void HostButtonClick() 
    {
        _eventAggregator.Publish(new MultiplayerConnectionData(_ipInputField.text, _portInputField.text, true));
        _sceneLoader.LoadSceneByType(SceneType.MultiplayerArena);
    }

    private void ClientButtonClick()
    {
        _eventAggregator.Publish(new MultiplayerConnectionData(_ipInputField.text, _portInputField.text, false));
        _sceneLoader.LoadSceneByType(SceneType.MultiplayerArena);
    }
}
