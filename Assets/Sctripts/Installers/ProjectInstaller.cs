using UnityEngine;
using UnityEngine.XR;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header ("\nScriptable Objects")]
    [SerializeField] private AllScenesData _allScenesData; 
    public override void InstallBindings()
    {
        Container.Bind<AllScenesData>().FromInstance(_allScenesData).AsSingle().NonLazy();

        Container.Bind<SceneLoader>().AsSingle().NonLazy();
    }
}