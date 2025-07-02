using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime.SpawnSystem
{
    public interface ISpawnMethod<TSpawnedGameObject> where TSpawnedGameObject : ISpawnedObject
    {
        UniTask StartWorkingAsync(ISpawner<TSpawnedGameObject> spawner, CancellationToken token);
    }
}