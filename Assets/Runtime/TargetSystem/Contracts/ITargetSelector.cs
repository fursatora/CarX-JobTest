using UnityEngine;

namespace Runtime.TargetSystem.Contracts
{
    public interface ITargetSelector
    {
        void Register(ITargetProvider provider);
        void Unregister(ITargetProvider provider);
        ITargetProvider GetNearest(Vector3 position);
    }
}