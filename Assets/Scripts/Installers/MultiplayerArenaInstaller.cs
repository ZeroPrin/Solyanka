using UnityEngine;
using Zenject;

public class MultiplayerArenaInstaller : MonoInstaller
{
    [SerializeField] Respawner _respawner;
    public override void InstallBindings()
    {
        Container.Bind<Respawner>().FromInstance(_respawner).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<MultiplayerArenaSceneController>().AsSingle().NonLazy();
    }
}
