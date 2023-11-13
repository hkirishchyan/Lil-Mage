using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private PlayerComponentManager _playerComponentManager; 
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerComponentManager>().FromInstance(_playerComponentManager).AsSingle();
        }
    }
}
