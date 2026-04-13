using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Edgar.Unity;

namespace QFramework.PG
{
    /// <summary>
    /// 同步 Edgar 共享 Tilemap 与房间模板 Tilemap 的 Tag/Layer。
    /// 用于修复默认共享层为 Untagged/Default 的问题。
    /// </summary>
    public class EdgarSharedTilemapTagLayerSync : DungeonGeneratorPostProcessingComponentGrid2D
    {
        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            if (level == null || level.RoomInstances == null || level.RoomInstances.Count == 0)
            {
                return;
            }

            var sharedTilemaps = level.GetSharedTilemaps();
            if (sharedTilemaps == null || sharedTilemaps.Count == 0)
            {
                return;
            }

            // 使用首个房间实例作为同名层的来源模板。
            var templateRoot = level.RoomInstances[0].RoomTemplateInstance;
            if (templateRoot == null)
            {
                return;
            }

            var sourceTilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(templateRoot);
            var sourceByName = new Dictionary<string, Tilemap>();
            foreach (var source in sourceTilemaps)
            {
                if (source != null && !sourceByName.ContainsKey(source.name))
                {
                    sourceByName.Add(source.name, source);
                }
            }

            foreach (var shared in sharedTilemaps)
            {
                if (shared == null) continue;
                if (!sourceByName.TryGetValue(shared.name, out var source)) continue;

                // 同步 Layer
                shared.gameObject.layer = source.gameObject.layer;

                // 同步 Tag（tag 不存在会抛错，做一次保护）
                try
                {
                    shared.gameObject.tag = source.gameObject.tag;
                }
                catch
                {
                    // 忽略无效 tag，避免影响主流程
                }
            }
        }
    }
}
