using System;

namespace Runtime.TargetSystem.Contracts
{
    public interface ITargetReceiver
    {
        bool IsReached { get; }
        ITargetProvider GetTarget();
        event Action<ITargetReceiver> OnTargetReached;
    }
}