using Module.System.Pooling.Base;
using UnityEngine;

namespace Module.System.Pooling.GameObjectPool
{
    public interface IPoolingGameObject : IPoolingObject
    {
        Transform Transform { get; }
    }
}
