using Runtime.MoveSystem.MoveTrajectories.Installer;
using Runtime.TargetSystem.Installer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime.GamePlay
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private MoveSystemInstaller _moveInstaller;
        [SerializeField] private TargetSystemInstaller _targetInstaller;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _moveInstaller.Install(builder);
            _targetInstaller.Install(builder);
        }
    }
}