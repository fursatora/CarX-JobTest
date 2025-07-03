using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime.SpawnSystem
{
    public class IntervalSpawnDespawnMethod<TSpawnedGameObject> : ISpawnMethod<TSpawnedGameObject>
        where TSpawnedGameObject : ISpawnedObject
    {
        private readonly ISpawner<TSpawnedGameObject> _spawner;

        private TimeSpan _spawnInterval;
        private TimeSpan _despawnInterval;

        public IntervalSpawnDespawnMethod(ISpawner<TSpawnedGameObject> spawner, float spawnInterval,
            float despawnInterval)
        {
            _spawner = spawner;
            _spawnInterval = TimeSpan.FromSeconds(spawnInterval);
            _despawnInterval = TimeSpan.FromSeconds(despawnInterval);
        }

        public async UniTask StartWorkingAsync(ISpawner<TSpawnedGameObject> spawner, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var obj = _spawner.Spawn();

                DespawnLater(obj, token).Forget();

                await UniTask.Delay(_spawnInterval, cancellationToken: token);
            }
        }

        private async UniTask DespawnLater(TSpawnedGameObject obj, CancellationToken token)
        {
            await UniTask.Delay(_despawnInterval, cancellationToken: token);
            if (!token.IsCancellationRequested)
            {
                 _spawner.Despawn(obj);
            }
        }
    }
}