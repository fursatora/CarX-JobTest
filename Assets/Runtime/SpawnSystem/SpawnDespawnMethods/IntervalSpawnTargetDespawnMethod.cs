using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.TargetSystem.Contracts;

namespace Runtime.SpawnSystem
{
    public class IntervalSpawnTargetDespawnMethod<TSpawnedGameObject> : ISpawnMethod<TSpawnedGameObject>
        where TSpawnedGameObject : ISpawnedObject
    {
        private readonly ISpawner<TSpawnedGameObject> _spawner;

        private TimeSpan _spawnInterval;

        public IntervalSpawnTargetDespawnMethod(ISpawner<TSpawnedGameObject> spawner, float spawnInterval)
        {
            _spawner = spawner;
            _spawnInterval = TimeSpan.FromMilliseconds(spawnInterval*1000);
        }

        public async UniTask StartWorkingAsync(ISpawner<TSpawnedGameObject> spawner, CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {
                var obj = _spawner.Spawn();
                
                if (obj is IHaveTargetReceiver holder)
                {
                    var receiver = holder.Receiver;
                    receiver.OnTargetReached += _ => _spawner.Despawn(obj);
                    
                }

                await UniTask.Delay(_spawnInterval,  cancellationToken : token);
            }
        }
        
    }
}