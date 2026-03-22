using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelGenerator : MonoBehaviour {
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
    /// 1:墙 e:敌人 p:玩家 空白:空地, #:门
    /// </summary>
    

    ///初始房间
    List<string> InitRoom{get; set;}=new List<string>
    {
        "1111111111",
        "1        1",
        "1        1",
        "1 p      1",
        "1         ",
        "1        1",
        "1        1",
        "1        1",
        "1        1",
        "1        1",
        "1111111111",
    };

    //普通房间
    List<string> NormalRoom{get; set;}=new List<string>
    {
        "1111111111",
        "1        1",
        "1 e      1",
        "1        1",
        "         ",
        "1      e 1",
        "1        1",
        "1        1",
        "1        1",
        "1        1",
        "1111111111",
    };

    //Boss房间
    List<string> BossRoom{get; set;}=new List<string>
    {
        "1111111111",
        "1        1",
        "1        1",
        "1        1",
        "         1",
        "1        1",
        "1        1",
        "1   #    1",
        "1        1",
        "1        1",
        "1111111111",
    };

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
        GenerateMap(InitRoom,curX,curY);
        curX = curX + InitRoom[0].Length + 2;
        GenerateMap(NormalRoom,curX,curY);
        curX = curX + NormalRoom[0].Length + 2;
        GenerateMap(BossRoom,curX,curY);

    }


    
    /// <summary>
    /// 生成地图
    /// </summary>
    private void GenerateMap(List<string> map,int startX,int startY) {
        for(int i = 0; i < map.Count; i++) {
            for(int j = 0; j < map[i].Length; j++) {
                int x = startX + j;
                int y = startY + i;
                //地板
                FloorTileMap.SetTile(new Vector3Int(x, y, 0), floorTile);
                //墙
                if(map[i][j] == '1') {
                    WallTileMap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
                //敌人
                if(map[i][j] == 'e') {
                    var obj = Instantiate(enemyPrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity);
                    obj.SetActive(true);
                }
                //玩家
                if(map[i][j] == 'p') {
                    var obj = Instantiate(playerPrefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity);
                    obj.SetActive(true);
                }
                if(map[i][j] == '#') {
                    var obj = Instantiate(doorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    obj.SetActive(true);
                }
            }
        }
    }
}
