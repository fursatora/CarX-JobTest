using Runtime.Gameplay.SpawnSystem.SpawnedObjects;
using Runtime.SpawnSystem;
using UnityEngine;

namespace Runtime.GamePlay.SpawnSystem
{
    public class IntervalSpawnTargetDespawnController : MainSpawnController
    {
        [SerializeField] private float spawnInterval;
        
        protected override ISpawnMethod<MainSpawnedObject> CreateSpawnMethod()
        {
           return new IntervalSpawnTargetDespawnMethod<MainSpawnedObject>(Spawner, spawnInterval);
        }
    }
}