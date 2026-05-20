using UnityEngine;
using UnityEngine.Tilemaps;
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Tiles/Random 4 Tile")]
    public class Random4Tile : Tile
    {
        public Sprite[] sprites = new Sprite[4];

        /// <summary>
        /// 执行 GetTileData 逻辑.
        /// </summary>
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            // 基于坐标生成稳定随机：同一格永远是同一张，不会每次刷新都变
            int hash = Mathf.Abs((position.x * 73856093) ^ (position.y * 19349663));
            int index = hash % sprites.Length;

            tileData.sprite = sprites[index];
            tileData.color = Color.white;
            tileData.transform = Matrix4x4.identity;
            tileData.flags = TileFlags.LockAll;
            tileData.colliderType = colliderType;
        }
    }
}
