namespace Module.System.Pooling.Base
{
    public interface IPoolListener<TPoolingObject>
    {
        TPoolingObject OnCreate();
        void OnGet(TPoolingObject poolingObject);
        void OnRelease(TPoolingObject poolingObject);
        void OnDispose(TPoolingObject poolingObject);
    }
}
