using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 敵のデータを管理するクラス </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/CreateEnemyDataAsset")]
public class EnemyData : ScriptableObject
{
    /// <summary> 敵のHP </summary>
    [Header ("敵のHP（Indexは現在のWave-1）")]
    public int[] m_enemyHP;
    /// <summary> 敵の移動スピード </summary>
    [Header ("敵のスピード（Indexは現在のWave-1）")]
    public float[] m_enemySpeed;
}
