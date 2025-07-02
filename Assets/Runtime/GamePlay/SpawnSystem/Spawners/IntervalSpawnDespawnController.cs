using Runtime.Gameplay.SpawnSystem.SpawnedObjects;
using Runtime.SpawnSystem;
using UnityEngine;

namespace Runtime.GamePlay.SpawnSystem
{
    public class IntervalSpawnDespawnController : MainSpawnController
    {
        [SerializeField] private float spawnInterval;
        [SerializeField] private float despawnInterval;

        protected override ISpawnMethod<MainSpawnedObject> CreateSpawnMethod()
        {
            return new IntervalSpawnDespawnMethod<MainSpawnedObject>(Spawner, spawnInterval, despawnInterval);
        }
    }
}