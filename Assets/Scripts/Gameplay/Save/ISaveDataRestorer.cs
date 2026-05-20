namespace Game.Gameplay.Save
{
    /// <summary>
    /// 后续模块化接入读档时使用的数据恢复接口.
    /// </summary>
    public interface ISaveDataRestorer<TSaveData>
    {
        /// <summary>
        /// 执行 RestoreSaveData 逻辑.
        /// </summary>
        void RestoreSaveData(TSaveData data);
    }
}
