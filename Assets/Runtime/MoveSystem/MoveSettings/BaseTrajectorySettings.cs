using Runtime.MoveSystem.Contracts;
using UnityEngine;

namespace Runtime.MoveSystem.MoveSettings
{
    public class BaseTrajectorySettings : ScriptableObject, ITrajectorySetting
    {
        [SerializeField] protected float speed;

        public float Speed => speed;
    }
}