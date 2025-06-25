using UnityEngine;

namespace Runtime.TargetSystem.Contracts
{
    public interface ITargetProvider
    {
        Vector3 Position { get; }
    }
}