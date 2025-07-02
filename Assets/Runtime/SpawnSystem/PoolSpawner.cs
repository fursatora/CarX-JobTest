using System;
using Module.System.Pooling.Base;

namespace Runtime.SpawnSystem
{
    public class PoolSpawner<TSpawnedGameObject> : ISpawner<TSpawnedGameObject>
        where TSpawnedGameObject : ISpawnedObject, IPoolingObject
    {
        public event Action<TSpawnedGameObject> OnSpawned;
        public event Action<TSpawnedGameObject> OnDespawned;

        private readonly IObjectPool<TSpawnedGameObject> _pool;

        public PoolSpawner(IObjectPool<TSpawnedGameObject> pool)
        {
            _pool = pool;
        }

        public TSpawnedGameObject Spawn()
        {
            TSpawnedGameObject instance = _pool.Get();
            OnSpawned?.Invoke(instance);
            return instance;
        }

        public void Despawn(TSpawnedGameObject instance)
        {
            _pool.Release(instance);
            OnDespawned?.Invoke(instance);
        }
    }
}