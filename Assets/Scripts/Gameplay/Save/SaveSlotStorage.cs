using System;
using System.IO;
using UnityEngine;

namespace Game.Gameplay.Save
{
    /// <summary>
    /// 存档槽位文件 IO, 不读取场景对象也不修改玩法状态.
    /// </summary>
    public static class SaveSlotStorage
    {
        private const string SaveFolderName = "Saves";
        private const string SaveFileNameFormat = "slot_{0}.json";
        private const string SnapshotFileNameFormat = "slot_{0}.png";

        public static string SaveFolderPath => Path.Combine(Application.persistentDataPath, SaveFolderName);
        public static string GetSlotPath(int slotIndex)
        {
            ValidateSlot(slotIndex);
            return Path.Combine(SaveFolderPath, string.Format(SaveFileNameFormat, slotIndex));
        }
        public static string GetSnapshotPath(int slotIndex)
        {
            ValidateSlot(slotIndex);
            return Path.Combine(SaveFolderPath, string.Format(SnapshotFileNameFormat, slotIndex));
        }
        public static bool SlotExists(int slotIndex)
        {
            return File.Exists(GetSlotPath(slotIndex));
        }
        public static GameSaveData ReadSlot(int slotIndex)
        {
            var path = GetSlotPath(slotIndex);
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameSaveData>(json);
        }
        public static void WriteSlot(int slotIndex, GameSaveData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Directory.CreateDirectory(SaveFolderPath);
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSlotPath(slotIndex), json);
        }
        public static bool DeleteSlot(int slotIndex)
        {
            var path = GetSlotPath(slotIndex);
            if (!File.Exists(path)) return false;

            File.Delete(path);
            var snapshotPath = GetSnapshotPath(slotIndex);
            if (File.Exists(snapshotPath))
            {
                File.Delete(snapshotPath);
            }

            return true;
        }
        public static SaveSlotSummary ReadSummary(int slotIndex)
        {
            var summary = new SaveSlotSummary
            {
                slotIndex = slotIndex,
                exists = SlotExists(slotIndex)
            };

            if (!summary.exists)
            {
                return summary;
            }

            var data = ReadSlot(slotIndex);
            if (data == null)
            {
                summary.exists = false;
                return summary;
            }

            summary.savedAtUtc = data.savedAtUtc;
            summary.sceneName = data.sceneName;
            summary.playerHp = data.player != null ? data.player.hp : 0;
            summary.playerMaxHp = data.player != null ? data.player.maxHp : 0;
            summary.currentRoomId = data.currentRoomId;
            summary.snapshotPath = GetSnapshotPath(slotIndex);
            return summary;
        }
        private static void ValidateSlot(int slotIndex)
        {
            if (slotIndex < 1 || slotIndex > SaveGameService.SlotCount)
            {
                throw new ArgumentOutOfRangeException(nameof(slotIndex), $"存档槽位必须在 1 到 {SaveGameService.SlotCount} 之间.");
            }
        }
    }
}
