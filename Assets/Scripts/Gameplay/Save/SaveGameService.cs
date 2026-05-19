using System;
using System.Collections.Generic;
using System.IO;
using Edgar.Unity;
using Game.Core;
using Game.Items;
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
        private const int SaveVersion = 1;
        private const string GameplaySceneName = "GameScene";
        private const int SnapshotWidth = 320;
        private const int SnapshotHeight = 180;
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

            var player = Global.player;
            if (player == null)
            {
                return SaveOperationResult.Fail("保存失败, 找不到玩家.");
            }

            var data = BuildSaveData(player);
            SaveSlotStorage.WriteSlot(slotIndex, data);
            CaptureSlotSnapshot(slotIndex);
            Debug.Log($"存档完成, Slot: {slotIndex}, Path: {SaveSlotStorage.GetSlotPath(slotIndex)}.");
            return SaveOperationResult.Ok("保存成功.", data);
        }

        public static SaveOperationResult LoadFromSlot(int slotIndex)
        {
            var data = SaveSlotStorage.ReadSlot(slotIndex);
            if (data == null)
            {
                return SaveOperationResult.Fail("读取失败, 槽位为空.");
            }

            // 读档统一重载 GameScene, 避免旧地图 seed, 敌人, 掉落物或房间实例残留.
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

        private static GameSaveData BuildSaveData(Player player)
        {
            var bootstrapper = UnityEngine.Object.FindObjectOfType<AddressableDungeonBootstrapper>();
            var currentRoom = ResolveCurrentRoom(player);
            var data = new GameSaveData
            {
                version = SaveVersion,
                savedAtUtc = DateTime.UtcNow.ToString("o"),
                sceneName = SceneManager.GetActiveScene().name,
                levelGraphAddress = bootstrapper != null ? bootstrapper.LevelGraphAddress : string.Empty,
                mapSeed = bootstrapper != null ? bootstrapper.LastGeneratedSeed : 0,
                currentRoomId = currentRoom != null ? currentRoom.SaveRoomId : string.Empty,
                player = CapturePlayer(player),
                rooms = CaptureRooms()
            };

            return data;
        }

        private static void CaptureSlotSnapshot(int slotIndex)
        {
            var camera = Camera.main;
            if (camera == null)
            {
                Debug.LogWarning("存档快照失败, 场景中没有 MainCamera.");
                return;
            }

            Directory.CreateDirectory(SaveSlotStorage.SaveFolderPath);
            var previousTarget = camera.targetTexture;
            var previousActive = RenderTexture.active;
            var renderTexture = RenderTexture.GetTemporary(SnapshotWidth, SnapshotHeight, 24);
            Texture2D texture = null;

            try
            {
                // 使用相机直接渲染缩略图, 避免把 Screen Space Overlay 的存档面板截进去.
                camera.targetTexture = renderTexture;
                RenderTexture.active = renderTexture;
                camera.Render();

                texture = new Texture2D(SnapshotWidth, SnapshotHeight, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0f, 0f, SnapshotWidth, SnapshotHeight), 0, 0);
                texture.Apply();
                File.WriteAllBytes(SaveSlotStorage.GetSnapshotPath(slotIndex), texture.EncodeToPNG());
            }
            finally
            {
                camera.targetTexture = previousTarget;
                RenderTexture.active = previousActive;
                RenderTexture.ReleaseTemporary(renderTexture);
                if (texture != null)
                {
                    UnityEngine.Object.Destroy(texture);
                }
            }
        }

        private static Room ResolveCurrentRoom(Player player)
        {
            if (Room.CurrentPlayerRoom != null)
            {
                return Room.CurrentPlayerRoom;
            }

            var rooms = UnityEngine.Object.FindObjectsOfType<Room>(true);
            var playerPosition = player.transform.position;
            for (var i = 0; i < rooms.Length; i++)
            {
                var room = rooms[i];
                if (room == null || room.SelfBoxCollider2D == null) continue;

                if (room.SelfBoxCollider2D.bounds.Contains(playerPosition))
                {
                    room.MarkVisited();
                    return room;
                }
            }

            return null;
        }

        public static void ApplyPendingGenerationSettings(AddressableDungeonBootstrapper bootstrapper, DungeonGeneratorGrid2D dungeonGenerator)
        {
            if (pendingLoadData == null || dungeonGenerator == null)
            {
                return;
            }

            // 生成前写入存档中的地图配方, 确保 Edgar 重建同一张地图.
            bootstrapper?.OverrideLevelGraphAddress(pendingLoadData.levelGraphAddress);
            dungeonGenerator.UseRandomSeed = false;
            dungeonGenerator.RandomGeneratorSeed = pendingLoadData.mapSeed;
        }

        public static bool TryRestorePendingSave()
        {
            if (pendingLoadData == null)
            {
                return false;
            }

            var result = RestoreSaveData(pendingLoadData);
            if (result.Success)
            {
                pendingLoadData = null;
            }

            return result.Success;
        }

        private static SaveOperationResult RestoreSaveData(GameSaveData data)
        {
            if (data == null)
            {
                return SaveOperationResult.Fail("读档失败, 存档数据为空.");
            }

            if (SceneManager.GetActiveScene().name != GameplaySceneName)
            {
                pendingLoadData = data;
                SceneManager.LoadScene(GameplaySceneName);
                return SaveOperationResult.Ok("正在进入游戏场景并恢复存档.", data);
            }

            RestoreRooms(data);

            var player = Global.player;
            if (player == null)
            {
                return SaveOperationResult.Fail("读档失败, 找不到玩家.");
            }

            player.RestoreSaveData(data.player);
            RestoreCurrentRoom(data.currentRoomId);
            Debug.Log($"读档完成, Scene: {data.sceneName}, Room: {data.currentRoomId}.");
            return SaveOperationResult.Ok("读档完成.", data);
        }

        private static void RestoreRooms(GameSaveData data)
        {
            if (data.rooms == null || data.rooms.Count == 0) return;

            var roomsById = new Dictionary<string, Room>();
            var rooms = UnityEngine.Object.FindObjectsOfType<Room>(true);
            for (var i = 0; i < rooms.Length; i++)
            {
                var room = rooms[i];
                if (room == null || string.IsNullOrWhiteSpace(room.SaveRoomId)) continue;

                roomsById[room.SaveRoomId] = room;
            }

            for (var i = 0; i < data.rooms.Count; i++)
            {
                var roomData = data.rooms[i];
                if (roomData == null || string.IsNullOrWhiteSpace(roomData.roomId)) continue;

                if (roomsById.TryGetValue(roomData.roomId, out var room))
                {
                    room.RestoreSaveData(roomData);
                }
                else
                {
                    Debug.LogWarning($"读档时找不到房间, RoomId: {roomData.roomId}.");
                }
            }
        }

        private static void RestoreCurrentRoom(string currentRoomId)
        {
            if (string.IsNullOrWhiteSpace(currentRoomId)) return;

            var rooms = UnityEngine.Object.FindObjectsOfType<Room>(true);
            for (var i = 0; i < rooms.Length; i++)
            {
                var room = rooms[i];
                if (room == null || room.SaveRoomId != currentRoomId) continue;

                Room.SetCurrentPlayerRoom(room);
                if (room.TryGetComponent<MinimapRoomData>(out var minimapData))
                {
                    minimapData.Highlight();
                }

                return;
            }
        }

        private static PlayerSaveData CapturePlayer(Player player)
        {
            var data = new PlayerSaveData
            {
                position = Vector3Data.FromVector3(player.transform.position),
                hp = player.HP,
                maxHp = player.MaxHP,
                currentGunIndex = player.currentGunIndex
            };

            CaptureInventory(player, data);
            CaptureBuffs(player, data);
            CaptureWeapons(player, data);
            return data;
        }

        private static void CaptureInventory(Player player, PlayerSaveData data)
        {
            var inventory = player.GetComponent<PlayerInventory>();
            if (inventory == null) return;

            for (var i = 0; i < inventory.Items.Count; i++)
            {
                var stack = inventory.Items[i];
                if (stack == null || stack.Count <= 0) continue;

                data.inventory.Add(new InventoryStackSaveData
                {
                    itemId = stack.ItemId,
                    count = stack.Count
                });
            }
        }

        private static void CaptureBuffs(Player player, PlayerSaveData data)
        {
            var manager = player.buffManager != null ? player.buffManager : player.GetComponent<BuffManager>();
            if (manager == null) return;

            var activeBuffs = manager.ActiveBuffs;
            for (var i = 0; i < activeBuffs.Count; i++)
            {
                var info = activeBuffs[i];
                if (info?.Buff == null) continue;

                data.buffs.Add(new BuffSaveData
                {
                    buffId = info.Buff.Id,
                    remainingTime = info.RemainingTime,
                    stackCount = info.StackCount,
                    isPermanent = info.IsPermanent
                });
            }
        }

        private static void CaptureWeapons(Player player, PlayerSaveData data)
        {
            for (var i = 0; i < player.guns.Count; i++)
            {
                var gun = player.guns[i];
                if (gun == null) continue;

                var clip = gun.GunClip;
                var bag = gun.BulletBag;
                data.weapons.Add(new WeaponSaveData
                {
                    weaponId = gun.WeaponId,
                    isCurrent = i == player.currentGunIndex,
                    clipAmmo = clip != null ? clip.currentAmmo : -1,
                    clipMaxAmmo = clip != null ? clip.maxAmmo : -1,
                    bagAmmo = bag != null ? bag.currentBullet : -1,
                    bagMaxAmmo = bag != null ? bag.maxBullet : -1
                });
            }
        }

        private static List<RoomSaveData> CaptureRooms()
        {
            var rooms = UnityEngine.Object.FindObjectsOfType<Room>(true);
            var result = new List<RoomSaveData>(rooms.Length);
            for (var i = 0; i < rooms.Length; i++)
            {
                var room = rooms[i];
                if (room == null) continue;

                result.Add(new RoomSaveData
                {
                    roomId = room.SaveRoomId,
                    roomType = room.GetType().Name,
                    position = Vector3Data.FromVector3(room.transform.position),
                    visited = room.Visited,
                    cleared = room.Cleared
                });
            }

            return result;
        }
    }
}
