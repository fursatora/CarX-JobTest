using System.Threading;
using Cysharp.Threading.Tasks;
using Module.Pooling.GameObjectPool;
using Runtime.Gameplay.SpawnSystem.SpawnedObjects;
using Runtime.SpawnSystem;
using UnityEngine;

namespace Runtime.GamePlay.SpawnSystem
{
    public abstract class MainSpawnController: MonoBehaviour
    {
        //[SerializeField] private float spawnInterval;
        [SerializeField] private MainSpawnedObject spawnedObjectPrefab;
        [SerializeField] private Transform poolContainer;
        [SerializeField] private Transform contentContainer;

        private ISpawner<MainSpawnedObject> _spawner;
        private ISpawnMethod<MainSpawnedObject> _spawnMethod;
        private GameObjectPool<MainSpawnedObject> _pool;
        private CancellationTokenSource _cts;
        
        public ISpawner<MainSpawnedObject> Spawner => _spawner;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            StartSpawning();
        }

        private void OnDisable()
        {
            StopSpawning();
        }

        private void Initialize()
        {
            var listener = new GameObjectPoolListener<MainSpawnedObject>(spawnedObjectPrefab, contentContainer);
            _pool = new GameObjectPool<MainSpawnedObject>(listener, poolContainer, contentContainer);
            _spawner = new PoolSpawner<MainSpawnedObject>(_pool);
            _spawnMethod = CreateSpawnMethod();
        }
        
        protected abstract ISpawnMethod<MainSpawnedObject> CreateSpawnMethod();

        private void StartSpawning()
        {
            if (_cts != null) return;
            _cts = new CancellationTokenSource();
            _spawnMethod.StartWorkingAsync(_spawner, _cts.Token).Forget();
        }

        private void StopSpawning()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}