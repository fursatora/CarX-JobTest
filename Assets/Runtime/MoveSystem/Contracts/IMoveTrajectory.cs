using UnityEngine;

namespace Runtime.MoveSystem.Contracts
{
    public interface IMoveTrajectory
    {
        void MoveStep(Transform transform, float deltaTime);
    }
}