using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorInfo
{
    /// <summary>
    /// 特定の座標の値を四捨五入し、Vector3Intにして返すメソッド
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    static public Vector3Int TankSetPositionToGrid(Vector3 clickPosition)
    {
        // x,z座標を切り上げ、切り捨てをする
        int floorX = Mathf.FloorToInt(clickPosition.x);
        int floorZ = Mathf.FloorToInt(clickPosition.z);
        int ceilX = Mathf.CeilToInt(clickPosition.x);
        int ceilZ = Mathf.CeilToInt(clickPosition.z);
        // 切り上げ切り捨てをしたものと、選択された座標の座分を求める
        float floorXDif = clickPosition.x - floorX;
        float floorZDif = clickPosition.x - floorZ;
        float ceilXDif = ceilX - clickPosition.x;
        float ceilZDif = ceilZ - clickPosition.x;

        if (floorXDif > ceilXDif)
        {
            if (floorZDif > ceilZDif)
            {
                int x = ceilX;
                int z = ceilZ;
                return new Vector3Int(x, 0, z);
            }
            else
            {
                int x = ceilX;
                int z = floorZ;
                return new Vector3Int(x, 0, z);
            }
        }
        else
        {
            if (floorZDif > ceilZDif)
            {
                int x = floorX;
                int z = ceilZ;
                return new Vector3Int(x, 0, z);
            }
            else
            {
                int x = floorX;
                int z = floorZ;
                return new Vector3Int(x, 0, z);
            }
        }
    }

    /// <summary> 戦車が設置されているかを判断する配列(0の場合>設置されていない。1の場合>設置されている。) </summary>
    static public int[,] mapInfo = new int[9, 4]
    {
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 }
    };
}
