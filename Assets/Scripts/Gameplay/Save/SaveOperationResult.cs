namespace Game.Gameplay.Save
{
    /// <summary>
    /// 存档操作返回值, 让 UI 显示明确成功或失败原因.
    /// </summary>
    public sealed class SaveOperationResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public GameSaveData Data { get; private set; }

        private SaveOperationResult(bool success, string message, GameSaveData data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 执行 Ok 逻辑.
        /// </summary>
        public static SaveOperationResult Ok(string message, GameSaveData data = null)
        {
            return new SaveOperationResult(true, message, data);
        }

        /// <summary>
        /// 执行 Fail 逻辑.
        /// </summary>
        public static SaveOperationResult Fail(string message)
        {
            return new SaveOperationResult(false, message, null);
        }
    }
}
