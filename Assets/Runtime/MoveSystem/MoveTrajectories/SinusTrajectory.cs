using Runtime.MoveSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveTrajectories
{
    public class SinusTrajectory : IMoveTrajectory
    {
        private readonly float _amplitude;
        private readonly float _frequency;
        private float _localTime;
        private float _prevOffset;
        private readonly float _speed;


        public SinusTrajectory(float speed, float amplitude, float frequency)
        {
            _speed = speed;
            _amplitude = amplitude;
            _frequency = frequency;
        }

        public void MoveStep(Transform transform, float deltaTime)
        {
            _localTime += _speed * deltaTime;

            transform.position += transform.forward * (_speed * deltaTime);

            var currentOffset = Mathf.Sin(_localTime * _frequency) * _amplitude;
            var deltaOffset = currentOffset - _prevOffset;

            transform.position += transform.up * deltaOffset;

            _prevOffset = currentOffset;
        }
    }
}