using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay.Save
{
    public static class SaveDataRestorer
    {
        public static async Task<SaveOperationResult> RestoreAsync(GameSaveData data, string gameplaySceneName)
        {
            if (data == null)
            {
                return SaveOperationResult.Fail("读档失败, 存档数据为空.");
            }

            if (SceneManager.GetActiveScene().name != gameplaySceneName)
            {
                SceneManager.LoadScene(gameplaySceneName);
                return SaveOperationResult.Fail("读档恢复等待游戏场景.");
            }

            RestoreRooms(data);

            var player = PlayerRegistry.Current;
            if (player == null)
            {
                return SaveOperationResult.Fail("读档失败, 找不到玩家.");
            }

            await player.RestoreSaveDataAsync(data.player);
            RestoreCurrentRoom(data.currentRoomId);
            Debug.Log($"读档完成, Scene: {data.sceneName}, Room: {data.currentRoomId}.");
            return SaveOperationResult.Ok("读档完成.", data);
        }

        private static void RestoreCurrentRoom(string currentRoomId)
        {
            if (string.IsNullOrWhiteSpace(currentRoomId))
            {
                return;
            }

            var rooms = Room.ActiveRooms;
            for (var i = 0; i < rooms.Count; i++)
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

        private static void RestoreRooms(GameSaveData data)
        {
            if (data.rooms == null || data.rooms.Count == 0)
            {
                return;
            }

            var roomsById = new Dictionary<string, Room>();
            var rooms = Room.ActiveRooms;
            for (var i = 0; i < rooms.Count; i++)
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
    }
}
