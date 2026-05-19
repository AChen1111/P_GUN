using System;

namespace Game.Gameplay.Save
{
    /// <summary>
    /// UI 列表使用的轻量槽位摘要.
    /// </summary>
    [Serializable]
    public class SaveSlotSummary
    {
        public int slotIndex;
        public bool exists;
        public string savedAtUtc;
        public string sceneName;
        public int playerHp;
        public int playerMaxHp;
        public string currentRoomId;
        public string snapshotPath;
    }
}
