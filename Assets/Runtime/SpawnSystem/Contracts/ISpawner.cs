using System;

namespace Runtime.SpawnSystem
{
    public interface ISpawner<TSpawnedGameObject> where TSpawnedGameObject : ISpawnedObject
    {
        event Action<TSpawnedGameObject> OnSpawned;
        event Action<TSpawnedGameObject> OnDespawned;
        
        TSpawnedGameObject Spawn();
        void Despawn(TSpawnedGameObject instance);
    }
}