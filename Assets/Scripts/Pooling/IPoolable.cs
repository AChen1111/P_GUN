namespace Game.Pooling
{
    public interface IPoolable
    {
        void OnSpawnFromPool();
        void OnRecycleToPool();
    }
}
