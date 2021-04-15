using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TankData", menuName = "ScriptableObjects/CreateTankInfoAsset")]
public class TankInfo : ScriptableObject
{
    [Header("戦車の移動可能範囲を設定")]
    public int m_maxX = 9; // X座標の最大値
    public int m_minX = 1; // X座標の最小値
    public int m_maxZ = 6; // Z座標の最大値
    public int m_minZ = 3; // Z座標の最小値
}

