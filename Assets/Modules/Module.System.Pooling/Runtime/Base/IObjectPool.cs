using System;

namespace Module.System.Pooling.Base
{
    public interface IObjectPool<TPoolingObject> : IDisposable where TPoolingObject : IPoolingObject
    {
        TPoolingObject Get();
        void Release(TPoolingObject poolingObject);
    }
}
