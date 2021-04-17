using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineTankMoveRange : MonoBehaviour
{
    /// <summary> 戦車の設置可能領域 </summary>
    float m_minX, m_maxX, m_minZ, m_maxZ;
    /// <summary> 線を引く高さ </summary>
    [SerializeField] float m_yPoint = 0.5f;
    /// <summary> 線の幅 </summary>
    [SerializeField] float m_width = 0.1f;
    /// <summary> TankInfoのScriptableObject </summary>
    [SerializeField] TankInfo m_tankInfo = null;
    /// <summary> LineRenderer </summary>
    LineRenderer m_line;

    /// <summary>
    /// 戦車の設置可能範囲に線を引く関数
    /// </summary>
    void DrawLineTankRange()
    {
        m_line = this.GetComponent<LineRenderer>();
        // 戦車の設置可能範囲より、少しだけ線を引いてできる四角形を大きくしている
        float shift = 0.5f; //ずらす大きさ
        m_minX = m_tankInfo.m_minX - shift;
        m_maxX = m_tankInfo.m_maxX + shift;
        m_minZ = m_tankInfo.m_minZ - shift;
        m_maxZ = m_tankInfo.m_maxZ + shift;

        // 頂点の座標リスト
        var positions = new Vector3[] {
            new Vector3(m_minX, m_yPoint, m_minZ),
            new Vector3(m_maxX, m_yPoint, m_minZ),
            new Vector3(m_maxX, m_yPoint, m_maxZ),
            new Vector3(m_minX, m_yPoint, m_maxZ)
        };

        m_line.positionCount = positions.Length;
        // 頂点の座標を設定
        m_line.SetPositions(positions);
        // 線の幅を設定
        m_line.startWidth = m_width;
        m_line.endWidth = m_width;
    }

    void Start()
    {
        DrawLineTankRange();
    }
}
