namespace Game.Gameplay.Save
{
    /// <summary>
    /// 后续模块化接入存档时使用的数据提供接口.
    /// </summary>
    public interface ISaveDataProvider<TSaveData>
    {
        TSaveData CaptureSaveData();
    }
}
