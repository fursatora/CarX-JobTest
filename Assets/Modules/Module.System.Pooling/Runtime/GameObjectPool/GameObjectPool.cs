using Module.System.Pooling.Base;
using Module.System.Pooling.GameObjectPool;
using UnityEngine;

namespace Module.Pooling.GameObjectPool
{
    public class GameObjectPool<TPoolingObject> : ObjectPool<TPoolingObject> where TPoolingObject : IPoolingGameObject
    {
        private const string PoolContainerName = "PoolContainer";

        private readonly Transform _poolContainer;
        private readonly Transform _contentContainer;

        public GameObjectPool(IPoolListener<TPoolingObject> listener, Transform poolContainer = null,
            Transform contentContainer = null) : base(listener)
        {
            _poolContainer = poolContainer;
            _contentContainer = contentContainer;
        }

        public override void Dispose()
        {
            base.Dispose();
            Object.Destroy(_poolContainer.gameObject);
        }

        public override TPoolingObject Get()
        {
            var poolingGameObject = base.Get();

            if (_contentContainer)
            {
                poolingGameObject.Transform.SetParent(_contentContainer, false);
                poolingGameObject.Transform.gameObject.SetActive(true);
            }

            return poolingGameObject;
        }

        public override void Release(TPoolingObject poolingGameObject)
        {
            if (poolingGameObject == null)
            {
                return;
            }
            poolingGameObject.Transform.gameObject.SetActive(false);
            poolingGameObject.Transform.SetParent(_poolContainer, false);

            base.Release(poolingGameObject);
        }

        private static Transform CreatePoolContainer(Transform parent, string containerName)
        {
            var instance = new GameObject($"{typeof(TPoolingObject).Name}:{containerName}", typeof(RectTransform));
            instance.SetActive(false);
            instance.transform.SetParent(parent, false);

            return instance.transform;
        }
    }
}