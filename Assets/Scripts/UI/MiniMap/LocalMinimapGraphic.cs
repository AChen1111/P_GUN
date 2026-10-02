using Game.Core;
using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// HUD 九宫格小地图, 直接绘制房间框和通路, 不依赖相机或地形复制层.
    /// </summary>
    public sealed class LocalMinimapGraphic : MaskableGraphic
    {
        [SerializeField] private Color backgroundColor = new Color(0.04f, 0.05f, 0.07f, 0.85f);
        [SerializeField] private Color unexploredColor = new Color(0.3f, 0.33f, 0.36f, 1f);
        [SerializeField] private Color visitedColor = new Color(0.2f, 0.8f, 0.35f, 1f);
        [SerializeField] private Color currentColor = new Color(1f, 0.85f, 0.15f, 1f);
        [SerializeField] private Color connectionColor = new Color(0.55f, 0.58f, 0.62f, 1f);
        private LocalMinimapSnapshot snapshot;

        protected override void OnEnable()
        {
            base.OnEnable();
            EventCenter.AddListener(GameplayEvents.LocalMinimapChanged, Refresh);
            Refresh();
        }

        protected override void OnDisable()
        {
            EventCenter.RemoveListener(GameplayEvents.LocalMinimapChanged, Refresh);
            snapshot = null;
            base.OnDisable();
        }

        private void Refresh()
        {
            // HUD 可先于异步房间生成启用, 生成完成事件会提供第一份快照.
            var generator = RandomRoomGenerator.Active;
            snapshot = generator != null ? generator.GetLocalMinimap() : null;
            SetVerticesDirty();
        }

        protected override void OnPopulateMesh(VertexHelper mesh)
        {
            mesh.Clear();
            var rect = GetPixelAdjustedRect();
            DrawRect(rect, backgroundColor);
            if (snapshot == null) return;
            var step = Mathf.Min(rect.width, rect.height) / 3f;
            var roomSize = step * 0.58f;
            var lineWidth = Mathf.Max(1f, step * 0.04f);
            foreach (var link in snapshot.Connections)
            {
                var from = rect.center + new Vector2(link.From.x, link.From.y) * step;
                var to = rect.center + new Vector2(link.To.x, link.To.y) * step;
                var horizontal = link.From.x != link.To.x;
                var middle = (from + to) * 0.5f;
                var size = horizontal ? new Vector2(step - roomSize, lineWidth) : new Vector2(lineWidth, step - roomSize);
                DrawRect(new Rect(middle - size * 0.5f, size), connectionColor);
            }
            foreach (var room in snapshot.Rooms)
            {
                var center = rect.center + new Vector2(room.Offset.x, room.Offset.y) * step;
                var bounds = new Rect(center - Vector2.one * roomSize * 0.5f, Vector2.one * roomSize);
                if (room.Current || room.Visited)
                {
                    DrawRect(bounds, room.Current ? currentColor : visitedColor);
                }
                else
                {
                    // 未探索房间只画边框, 内部保持背景色.
                    DrawRect(bounds, backgroundColor);
                    DrawRect(new Rect(bounds.xMin, bounds.yMin, bounds.width, lineWidth), unexploredColor);
                    DrawRect(new Rect(bounds.xMin, bounds.yMax - lineWidth, bounds.width, lineWidth), unexploredColor);
                    DrawRect(new Rect(bounds.xMin, bounds.yMin, lineWidth, bounds.height), unexploredColor);
                    DrawRect(new Rect(bounds.xMax - lineWidth, bounds.yMin, lineWidth, bounds.height), unexploredColor);
                }
            }

            void DrawRect(Rect bounds, Color tint)
            {
                var start = mesh.currentVertCount;
                var finalColor = (Color32)(tint * color);
                mesh.AddVert(new Vector3(bounds.xMin, bounds.yMin), finalColor, Vector2.zero);
                mesh.AddVert(new Vector3(bounds.xMin, bounds.yMax), finalColor, Vector2.zero);
                mesh.AddVert(new Vector3(bounds.xMax, bounds.yMax), finalColor, Vector2.zero);
                mesh.AddVert(new Vector3(bounds.xMax, bounds.yMin), finalColor, Vector2.zero);
                mesh.AddTriangle(start, start + 1, start + 2);
                mesh.AddTriangle(start, start + 2, start + 3);
            }
        }
    }
}
