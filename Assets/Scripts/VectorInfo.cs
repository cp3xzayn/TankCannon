using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorInfo
{
    /// <summary>
    /// クリックされたポジションの値を切り下げて、戦車をセットするポジションを決める
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    static public Vector3Int TankSetPosition(Vector3 clickPosition)
    {
        int x = Mathf.FloorToInt(clickPosition.x);
        int z = Mathf.FloorToInt(clickPosition.z);
        return new Vector3Int(x, 1, z);
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
