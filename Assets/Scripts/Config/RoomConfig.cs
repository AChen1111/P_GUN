using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QFramework.PG
{

    public enum RoomTypes
    {
        Init,
        Normal,
        Chest,
        Shop,
        Final,
        Next,
        Complete,
    }

    public class RoomConfig
    {
        //敌人波数
        public int waveNum = -1;
        public RoomTypes roomType;
        public List<string> codes = new List<string>();

        public int Height => codes.Count;
        public int Width => codes[0].Length;

        public RoomConfig Type(RoomTypes type)
        {
            roomType = type;
            return this;
        }

        public RoomConfig L(string code)
        {
            codes.Add(code);
            return this;
        }

        public RoomConfig WaveNum(int waveNum)
        {
            this.waveNum = waveNum;
            return this;
        }

    }


    /// <summary>
    /// 示例房间
    /// </summary>
    public class SampleRoom
    {
     /// 1:墙 e:敌人 p:玩家 空白:空地, #:出口 d:门
        public static RoomConfig InitRoom = new RoomConfig().Type(RoomTypes.Init).
        L("1111111111").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1 p      1").
        L("1        d").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1111111111");

        public static RoomConfig NormalRoom = new RoomConfig().Type(RoomTypes.Normal).
        L("1111111111").
        L("1        1").
        L("1 e      1").
        L("1        1").
        L("d        d").
        L("1      e 1").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1111111111").
        WaveNum(3);


        public static RoomConfig FinalRoom = new RoomConfig().Type(RoomTypes.Final).
        L("1111111111").
        L("1        1").
        L("1   #    1").
        L("1        1").
        L("d        1").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1        1").
        L("1111111111");


        public static RoomConfig GetRoomConfigWithType(RoomTypes type)
        {
            switch(type)
            {
                case RoomTypes.Init:
                    return InitRoom;
                case RoomTypes.Normal:
                    return NormalRoom;
                default:
                    throw new System.Exception($"RoomConfig not found: {type}");
            }
        }
    }
}
