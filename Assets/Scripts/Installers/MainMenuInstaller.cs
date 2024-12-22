using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MainMenuSceneController>().AsSingle().NonLazy();
    }
}
