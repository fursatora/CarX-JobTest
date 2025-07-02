using Module.System.Pooling.Base;
using UnityEngine;

namespace Runtime.SpawnSystem
{
    public class GameObjectPoolListener<TSpawnedGameObject> : IPoolListener<TSpawnedGameObject> where TSpawnedGameObject : Object, ISpawnedObject
    {
        private readonly TSpawnedGameObject _spawnedObjectPrefab;
        private readonly Transform _contentContainer;
        
        public GameObjectPoolListener(TSpawnedGameObject prefab, Transform contentContainer)
        {
            _spawnedObjectPrefab = prefab;
            _contentContainer = contentContainer;
        }
        
        public TSpawnedGameObject OnCreate()
        {
            return Object.Instantiate(_spawnedObjectPrefab, _contentContainer);
        }

        public void OnGet(TSpawnedGameObject poolingObject)
        {
            poolingObject.Transform.SetParent(_contentContainer);
            poolingObject.Transform.localPosition = Vector3.zero;
        }

        public void OnRelease(TSpawnedGameObject poolingObject)
        {
        }

        public void OnDispose(TSpawnedGameObject poolingObject)
        {
        }
    }
}