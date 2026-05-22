using System;
using System.Collections.Generic;
using Game.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay.Save
{
    public static class SaveDataBuilder
    {
        public static GameSaveData Build(Player player, int saveVersion)
        {
            var bootstrapper = AddressableDungeonBootstrapper.Active;
            var currentRoom = ResolveCurrentRoom(player);
            return new GameSaveData
            {
                version = saveVersion,
                savedAtUtc = DateTime.UtcNow.ToString("o"),
                sceneName = SceneManager.GetActiveScene().name,
                levelGraphAddress = bootstrapper != null ? bootstrapper.LevelGraphAddress : string.Empty,
                mapSeed = bootstrapper != null ? bootstrapper.LastGeneratedSeed : 0,
                currentRoomId = currentRoom != null ? currentRoom.SaveRoomId : string.Empty,
                player = CapturePlayer(player),
                rooms = CaptureRooms()
            };
        }

        private static List<RoomSaveData> CaptureRooms()
        {
            var rooms = Room.ActiveRooms;
            var result = new List<RoomSaveData>(rooms.Count);
            for (var i = 0; i < rooms.Count; i++)
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

        private static Room ResolveCurrentRoom(Player player)
        {
            if (Room.CurrentPlayerRoom != null)
            {
                return Room.CurrentPlayerRoom;
            }

            var rooms = Room.ActiveRooms;
            var playerPosition = player.transform.position;
            for (var i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (room == null || room.SelfBoxCollider2D == null) continue;
                if (!room.SelfBoxCollider2D.bounds.Contains(playerPosition)) continue;

                room.MarkVisited();
                return room;
            }

            return null;
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
    }
}
