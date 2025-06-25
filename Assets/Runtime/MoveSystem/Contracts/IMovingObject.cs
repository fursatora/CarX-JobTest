using UnityEngine;

namespace Runtime.MoveSystem.Contracts
{
    public interface IMovingObject
    {
        Transform Transform { get; }
    }
}