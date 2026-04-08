using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace QFramework.PG {
    public partial class LevelGenerator : ViewController {
        [Header("Tile")]
        public List<TileBase> WallTiles;
        public List<TileBase> FloorTiles;
        public Tilemap WallTileMap;
        public Tilemap FloorTileMap;

        [Header("预制体")]
        public GameObject enemyPrefab;
        public GameObject playerPrefab;
        public GameObject doorPrefab;
        
        /// <summary>
        /// 地图数据
        /// 1:墙 e:敌人 p:玩家 空白:空地, #:出口 d:门
        /// </summary>
        

        // ///初始房间
        // List<string> InitRoom{get; set;}=new List<string>
        // {
        //     "1111111111",
        //     "1        1",
        //     "1        1",
        //     "1 p      1",
        //     "1        d",
        //     "1        1",
        //     "1        1",
        //     "1        1",
        //     "1        1",
        //     "1        1",
        //     "1111111111",
        // };

        // //普通房间
        // List<string> NormalRoom{get; set;}=new List<string>
        // {
        //     "1111111111",
        //     "1        1",
        //     "1 e      1",
        //     "1        1",
        //     "d        d",
        //     "1      e 1",
        //     "1        1",
        //     "1        1",
        //     "1        1",
        //     "1        1",
        //     "1111111111",
        // };

        // //Boss房间
        // List<string> BossRoom{get; set;}=new List<string>
        // {
        //     "1111111111",
        //     "1        1",
        //     "1        1",
        //     "1        1",
        //     "d        1",
        //     "1        1",
        //     "1        1",
        //     "1   #    1",
        //     "1        1",
        //     "1        1",
        //     "1111111111",
        // };

        /// <summary>
        /// 随机墙砖
        /// </summary>
        private TileBase wallTile => WallTiles[Random.Range(0, WallTiles.Count)];
        /// <summary>
        /// 随机地板砖
        /// </summary>
        private TileBase floorTile => FloorTiles[Random.Range(0, FloorTiles.Count)];

        private void Start() {
            int curX = 0;
            int curY = 0;
            GenerateMap(SampleRoom.InitRoom,curX,curY);
            curX = curX + SampleRoom.InitRoom.Width + 2;
            GenerateMap(SampleRoom.NormalRoom,curX,curY);
            curX = curX + SampleRoom.NormalRoom.Width + 2;
            GenerateMap(SampleRoom.FinalRoom,curX,curY);
        }


        
        /// <summary>
        /// 生成地图
        /// </summary>
        private void GenerateMap(RoomConfig roomConfig,int startX,int startY) {
            //房间宽高
            var roomWidth = roomConfig.Width;
            var roomHeight = roomConfig.Height;

            //房间中心点
            var roomPosX = startX + roomWidth / 2;
            var roomPosY = startY + roomHeight / 2;

            //房间实例化
            var room = Instantiate(Room,transform,false);

 
            //遍历地图
            for(int i = 0; i < roomConfig.Height; i++) {
                for(int j = 0; j < roomConfig.Width; j++) {
                    int x = startX + j;
                    int y = startY + i;
                    //地板
                    FloorTileMap.SetTile(new Vector3Int(x, y, 0), floorTile);
                    switch (roomConfig.codes[i][j]) {
                        case '1':
                            WallTileMap.SetTile(new Vector3Int(x, y, 0), wallTile);
                            break;
                        case 'e':
                            room.AddEnemy(new Vector3(x + 0.5f, y + 0.5f, 0));
                            break;
                        case 'p':
                            var playerObj = Instantiate(playerPrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity);
                            playerObj.SetActive(true);
                            break;
                        case '#':
                            var doorObj = Instantiate(doorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                            doorObj.SetActive(true);
                            break;
                        case 'd':
                            room.AddDoor(new Vector3(x + 0.5f, y + 0.5f , 0));
                            break;
                    }
                    }
                }

                //初始化配置
                room.InitRoom(roomPosX,roomPosY,roomConfig);
            }
        }
}

