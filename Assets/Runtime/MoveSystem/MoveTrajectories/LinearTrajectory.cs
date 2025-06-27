using Runtime.MoveSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveTrajectories
{
    public class LinearTrajectory : IMoveTrajectory
    {
        private readonly float _speed;

        public LinearTrajectory(float speed)
        {
            _speed = speed;
        }

        public void MoveStep(Transform transform, float deltaTime)
        {
            transform.Translate(Vector3.forward * _speed * deltaTime, Space.Self);
        }
    }
}