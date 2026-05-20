namespace Game.Pooling
{
    public interface IPoolable
    {
        /// <summary>
        /// 执行 OnSpawnFromPool 逻辑.
        /// </summary>
        void OnSpawnFromPool();
        /// <summary>
        /// 执行 OnRecycleToPool 逻辑.
        /// </summary>
        void OnRecycleToPool();
    }
}
