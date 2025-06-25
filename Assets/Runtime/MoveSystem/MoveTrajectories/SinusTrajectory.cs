using Runtime.MoveSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveTrajectories
{
    public class SinusTrajectory : IMoveTrajectory
    {
        private float _speed;
        private float _amplitude;
        private float _frequency;
        private float _localTime;
        private float _prevOffset;


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

            float currentOffset = Mathf.Sin(_localTime * _frequency) * _amplitude;
            float deltaOffset = currentOffset - _prevOffset;

            transform.position += transform.up * deltaOffset;

            _prevOffset = currentOffset;
        }
    }
}