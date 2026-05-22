namespace Game.Gameplay
{
    /// <summary>
    /// Buff 脚本实例工厂抽象, 由具体 Lua 运行时在 Root 场景注册.
    /// </summary>
    public interface IBuffScriptFactory
    {
        /// <summary>
        /// 根据 Buff 配置创建脚本实例.
        /// </summary>
        /// <param name="buff">Buff 配置.</param>
        /// <returns>脚本实例.</returns>
        IBuffScriptInstance CreateBuffInstance(Buff buff);
    }
}
