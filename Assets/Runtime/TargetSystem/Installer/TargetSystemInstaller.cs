using Runtime.TargetSystem.Contracts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Runtime.TargetSystem.Installer
{
    public class TargetSystemInstaller : MonoBehaviour, IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            //builder.Register<TargetSelector>(Lifetime.Singleton)
                //.As<ITargetSelector>();
        }
    }
}