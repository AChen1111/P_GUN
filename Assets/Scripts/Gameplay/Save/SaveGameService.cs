using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Edgar.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay.Save
{
    /// <summary>
    /// 存档系统门面, UI 只通过这里执行存档槽位操作.
    /// </summary>
    public static class SaveGameService
    {
        public const int SlotCount = 3;
        internal const int SaveVersion = 1;
        internal const string GameplaySceneName = "GameScene";

        private static GameSaveData pendingLoadData;

        public static IReadOnlyList<SaveSlotSummary> GetSlotSummaries()
        {
            var summaries = new List<SaveSlotSummary>(SlotCount);
            for (var slot = 1; slot <= SlotCount; slot++)
            {
                summaries.Add(SaveSlotStorage.ReadSummary(slot));
            }

            return summaries;
        }

        public static SaveOperationResult SaveToSlot(int slotIndex)
        {
            if (SceneManager.GetActiveScene().name != GameplaySceneName)
            {
                return SaveOperationResult.Fail("只有游戏场景可以保存.");
            }

            if (FightRoom.currentFightRoom != null)
            {
                return SaveOperationResult.Fail("战斗中不能保存.");
            }

            var player = PlayerRegistry.Current;
            if (player == null)
            {
                return SaveOperationResult.Fail("保存失败, 找不到玩家.");
            }

            var data = SaveDataBuilder.Build(player, SaveVersion);
            SaveSlotStorage.WriteSlot(slotIndex, data);
            SaveSlotSnapshotCapture.Capture(slotIndex);
            Debug.Log($"存档完成, Slot: {slotIndex}, Path: {SaveSlotStorage.GetSlotPath(slotIndex)}.");
            return SaveOperationResult.Ok("保存成功.", data);
        }

        /// <summary>
        /// 异步读档入口, 主菜单读档前先确保全局数据库已经加载.
        /// </summary>
        public static async Task<SaveOperationResult> LoadFromSlotAsync(int slotIndex)
        {
            var data = SaveSlotStorage.ReadSlot(slotIndex);
            if (data == null)
            {
                return SaveOperationResult.Fail("读取失败, 槽位为空.");
            }

            // 玩法数值改由 Lua 数据表提供, 读档不再预载 ScriptableObject 数据库.
            pendingLoadData = data;
            SceneManager.LoadScene(GameplaySceneName);
            return SaveOperationResult.Ok("正在进入游戏场景并恢复存档.", data);
        }

        public static SaveOperationResult DeleteSlot(int slotIndex)
        {
            var deleted = SaveSlotStorage.DeleteSlot(slotIndex);
            return deleted
                ? SaveOperationResult.Ok("删除成功.")
                : SaveOperationResult.Fail("删除失败, 槽位为空.");
        }

        /// <summary>
        /// 生成前把存档中的关卡 id 和种子写入随机房间生成器, 确保重建同一张地图.
        /// </summary>
        /// <param name="generator">场景中的随机房间生成器.</param>
        public static void ApplyPendingGenerationSettings(RandomRoomGenerator generator)
        {
            if (pendingLoadData == null || generator == null)
            {
                return;
            }

            generator.OverrideLevel(pendingLoadData.levelId, pendingLoadData.mapSeed);
        }

        /// <summary>
        /// 异步恢复待读档数据, 允许玩家按需加载武器和背包效果资源.
        /// </summary>
        public static async Task<bool> TryRestorePendingSaveAsync()
        {
            if (pendingLoadData == null)
            {
                return false;
            }

            var result = await SaveDataRestorer.RestoreAsync(pendingLoadData, GameplaySceneName);
            if (result.Success)
            {
                pendingLoadData = null;
            }

            return result.Success;
        }
    }
}
