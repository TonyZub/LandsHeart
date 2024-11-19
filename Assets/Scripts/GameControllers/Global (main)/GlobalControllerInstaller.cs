using UnityEngine;
using Zenject;

namespace LandsHeart
{
    public sealed class GlobalControllerInstaller : MonoInstaller
    {
        [SerializeField] private GlobalController _globalControllerPrefab;

        public override void InstallBindings()
        {
            var globalControllerInstance = Container.InstantiatePrefab(_globalControllerPrefab);
            Container.Bind<GlobalController>().FromComponentInChildren(globalControllerInstance).AsSingle();
        }
    }
}

