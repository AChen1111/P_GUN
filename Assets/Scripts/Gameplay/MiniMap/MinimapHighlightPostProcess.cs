using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
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
    /// Edgar 地牢生成后处理：
    /// 1. 在 MiniMap 层新建一个「高亮」Tilemap
    /// 2. 为每个房间计算其 Floor tile 在共享坐标系中的位置
    /// 3. 将位置列表和高亮 Tilemap 赋值给房间上的 MinimapRoomData 组件
    ///
    /// 使用方法：
    /// ① Create -> PG -> MinimapHighlightPostProcess 创建资源
    /// ② 将该资源拖到 DungeonGenerator 的 Custom post-process tasks（放在 MinimapPostProcess 之后）
    /// ③ 将 MiniMapLayer 设为你创建的 "MiniMap" Layer
    /// </summary>
    [CreateAssetMenu(menuName = "PG/Minimap Highlight Post Process", fileName = "MinimapHighlightPostProcess")]
    public class MinimapHighlightPostProcess : DungeonGeneratorPostProcessingGrid2D
    {
        [Header("MiniMap 专用 Layer（需与 MinimapPostProcess 一致）")]
        [Edgar.Unity.Layer]
        public int MiniMapLayer = 0;

        [Header("玩家所在房间高亮颜色")]
        public Color HighlightColor = Color.yellow;

        [Header("Floor Tilemap 名称（与房间模板中一致）")]
        public string FloorTilemapName = "Floor";

        /// <summary>
        /// 执行 Run 逻辑.
        /// </summary>
        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            // 在共享 Tilemap 根节点下新建高亮层
            var tilemapsRoot = level.RootGameObject.transform
                .Find(GeneratorConstantsGrid2D.TilemapsRootName);

            if (tilemapsRoot == null)
            {
                Debug.LogWarning("[MinimapHighlight] 找不到 TilemapsRoot，请检查 DungeonGenerator 配置。");
                return;
            }

            var highlightObj = new GameObject("Minimap Highlight");
            highlightObj.transform.SetParent(tilemapsRoot);
            highlightObj.transform.localPosition = Vector3.zero;
            highlightObj.layer = MiniMapLayer;

            var highlightTilemap = highlightObj.AddComponent<Tilemap>();
            var rend = highlightObj.AddComponent<TilemapRenderer>();
            // 比普通 Minimap 层更高的排序，确保显示在最上层
            rend.sortingOrder = 30;

            // 为每个房间（非走廊）计算 Floor tile 位置
            foreach (var roomInstance in level.RoomInstances)
            {
                if (roomInstance.IsCorridor) continue;

                var roomTilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(
                    roomInstance.RoomTemplateInstance);

                var floorTilemap = roomTilemaps.FirstOrDefault(
                    t => t.name == FloorTilemapName);

                if (floorTilemap == null)
                {
                    Debug.LogWarning(
                        $"[MinimapHighlight] 房间 '{roomInstance.Room?.GetDisplayName()}' " +
                        $"找不到名为 '{FloorTilemapName}' 的 Tilemap，已跳过。");
                    continue;
                }

                // 将 Room 本地坐标转为共享坐标系坐标
                var positions = new List<Vector3Int>();
                foreach (var localPos in floorTilemap.cellBounds.allPositionsWithin)
                {
                    if (floorTilemap.HasTile(localPos))
                        positions.Add(localPos + roomInstance.Position);
                }

                var data = roomInstance.RoomTemplateInstance.GetComponent<MinimapRoomData>();
                if (data == null)
                {
                    throw new System.InvalidOperationException($"{nameof(MinimapRoomData)} must be configured on room template instance.");
                }

                data.Positions = positions;
                data.HighlightTilemap = highlightTilemap;
                data.HighlightColor = HighlightColor;
            }
        }
    }
}
