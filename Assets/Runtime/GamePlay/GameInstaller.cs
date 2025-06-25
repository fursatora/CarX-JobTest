using Runtime.MoveSystem.MoveTrajectories.Installer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime.GamePlay
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] MoveSystemInstaller _moveInstaller;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _moveInstaller.Install(builder);
        }
    }
}