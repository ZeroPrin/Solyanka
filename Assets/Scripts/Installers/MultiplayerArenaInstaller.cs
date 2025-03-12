using UnityEngine;
using Zenject;

public class MultiplayerArenaInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MultiplayerArenaSceneController>().AsSingle().NonLazy();
    }
}
