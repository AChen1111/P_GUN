using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    /// <summary>
    /// 挂在每个房间 RoomTemplateInstance 上，
    /// 记录该房间在 Minimap 高亮层的 tile 位置，并负责高亮/清除逻辑。
    /// </summary>
    public class MinimapRoomData : MonoBehaviour
    {
        // 该房间在共享 tilemap 坐标系中的 Floor tile 位置列表
        public List<Vector3Int> Positions = new List<Vector3Int>();

        // 专门用于高亮显示的 Tilemap（由后处理脚本创建并赋值）
        public Tilemap HighlightTilemap;

        public Color HighlightColor = Color.yellow;

        // 当前正在高亮的房间（全局唯一）
        private static MinimapRoomData currentHighlightedRoom;

        // 高亮用的单色 Tile（复用同一个）
        private static Tile highlightTile;

        /// <summary>
        /// 高亮此房间，同时取消上一个房间的高亮。
        /// </summary>
        public void Highlight()
        {
            if (currentHighlightedRoom != null && currentHighlightedRoom != this)
                currentHighlightedRoom.ClearHighlight();

            currentHighlightedRoom = this;
            Paint();

            void Paint()
            {
                if (HighlightTilemap == null || Positions == null)
                    return;
                EnsureHighlightTile();
                HighlightTilemap.ClearAllTiles();
                foreach (var pos in Positions)
                    HighlightTilemap.SetTile(pos, highlightTile);
            }

    void EnsureHighlightTile()
    {
        if (highlightTile != null)
            return;
        highlightTile = ScriptableObject.CreateInstance<Tile>();
        var tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, HighlightColor);
        tex.Apply();
        highlightTile.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
    }
}

        /// <summary>
        /// 清除此房间的高亮。
        /// </summary>
        public void ClearHighlight()
        {
            HighlightTilemap?.ClearAllTiles();
            if (currentHighlightedRoom == this)
                currentHighlightedRoom = null;
        }
    }
}
