using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime.MoveSystem.MoveTrajectories.Installer
{
    public class MoveSystemInstaller: MonoBehaviour, IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ScriptableTrajectoriesFactory>(Lifetime.Singleton)
                .AsSelf();
        }
    }
}