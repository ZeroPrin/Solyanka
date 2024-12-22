using UnityEngine;
using UnityEngine.XR;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header ("\n Scriptable Objects")]
    [SerializeField] private AllScenesData _allScenesData;

    [Header("\n UI Manager")]
    [SerializeField] private UIManager _uiManager;

    public override void InstallBindings()
    {
        Container.Bind<AllScenesData>().FromInstance(_allScenesData).AsSingle().NonLazy();
        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle().NonLazy();

        Container.Bind<SceneLoader>().AsSingle().NonLazy();
    }
}