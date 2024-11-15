using UnityEngine;
using Zenject;

public class LoadInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LoadSceneController>().AsSingle().NonLazy();
    }
}
