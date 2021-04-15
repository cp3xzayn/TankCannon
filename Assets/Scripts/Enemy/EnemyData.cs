using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のデータを管理するクラス
/// </summary>
public class EnemyData
{
    private int m_enemyHP = 2;
    public int EnemyHP
    {
        set { m_enemyHP = value; }
        get { return m_enemyHP; }
    }
}
