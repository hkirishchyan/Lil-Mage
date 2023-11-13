namespace ObjectPool
{
    public interface IPoolable<T>
    {
        void InitializePool(System.Action<T> returnAction);
        void ReturnToPool();
    }
}