using System;

namespace Runtime.TargetSystem.Contracts
{
    public interface ITargetReceiver
    {
        ITargetProvider GetTarget();
        event Action<ITargetReceiver> OnTargetReached;
        bool IsReached { get; }
    }
}