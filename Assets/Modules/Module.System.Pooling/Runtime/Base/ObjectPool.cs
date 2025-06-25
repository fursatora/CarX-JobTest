using System.Collections.Generic;
using System.Linq;

namespace Module.System.Pooling.Base
{
    public class ObjectPool<TPoolingObject> : IObjectPool<TPoolingObject> where TPoolingObject : IPoolingObject
    {
        private readonly IPoolListener<TPoolingObject> _listener;
        private readonly Stack<TPoolingObject> _stack = new();
        
        public ObjectPool(IPoolListener<TPoolingObject> listener)
        {
            _listener = listener;
        }

        public virtual void Dispose()
        {
            while (_stack.Any())
            {
                var poolingObject = _stack.Pop();
                _listener.OnDispose(poolingObject);
            }
        }

        public virtual TPoolingObject Get()
        {
            TPoolingObject poolingObject;
            
            if (_stack.Any())
            {
                poolingObject = _stack.Pop();
                _listener.OnGet(poolingObject);
            }
            else
            {
                poolingObject = _listener.OnCreate();
            }
            
            return poolingObject;
        }

        public virtual void Release(TPoolingObject poolingObject)
        {
            _stack.Push(poolingObject);
            _listener.OnRelease(poolingObject);
        }
    }
}
