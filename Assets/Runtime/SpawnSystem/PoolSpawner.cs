using System;
using Module.System.Pooling.Base;
using UnityEngine;

namespace Runtime.SpawnSystem
{
    public class PoolSpawner<TSpawnedGameObject> : ISpawner<TSpawnedGameObject>
        where TSpawnedGameObject : ISpawnedObject, IPoolingObject
    {
        public event Action<TSpawnedGameObject> OnSpawned;
        public event Action<TSpawnedGameObject> OnDespawned;

        private readonly IObjectPool<TSpawnedGameObject> _pool;
        private readonly Transform _spawnPoint;

        public PoolSpawner(IObjectPool<TSpawnedGameObject> pool, Transform spawnPoint)
        {
            _pool = pool;
            _spawnPoint = spawnPoint;
        }

        public TSpawnedGameObject Spawn()
        {
            TSpawnedGameObject instance = _pool.Get();
            instance.Transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);
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