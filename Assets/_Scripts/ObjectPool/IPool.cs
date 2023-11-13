namespace ObjectPool
{
    public interface IPool<T>
    {
        T Pull();
        void Push(T pushObject);
    }
}