using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenerator : MonoBehaviour
{
    /// <summary> 生成する戦車のオブジェクト </summary>
    [SerializeField] GameObject m_tank = null;
    [SerializeField] GameObject m_targetMarker = null;

    void Start()
    {
        m_targetMarker.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Prepare)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Field")
                {
                    MarkerAction(hit.point);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        InstantiateTank(hit.point);
                    }
                }
            }
        }
    }

    /*-----------------------------------------*/
    // 戦車の生成可能範囲の情報
    [Header("戦車の生成可能範囲の情報")]
    [SerializeField] int m_maxX = 9;
    [SerializeField] int m_minX = 1;
    [SerializeField] int m_maxZ = 6;
    [SerializeField] int m_minZ = 3;
    /*-----------------------------------------*/

    /// <summary>
    /// 戦車を生成する
    /// </summary>
    /// <param name="hitPos"></param>
    void InstantiateTank(Vector3 hitPos)
    {
        // 戦車をセットする場所のGrid座標
        Vector3Int setPosGrid = VectorInfo.TankSetPositionToGrid(hitPos);
        // 設置するためにVector3に変換
        Vector3 setPos = TankSetPosition(setPosGrid);
        // 戦車が生成可能範囲が選択された場合（現状IndexOutOfrangeが出てしまってる　＝＞　修正する必要あり）
        // 戦車設置可能かを判断し、可能なら戦車を生成する。
        if (VectorInfo.mapInfo[setPosGrid.x - m_minX, setPosGrid.z - m_minZ] == 0)
        {
            VectorInfo.mapInfo[setPosGrid.x - m_minX, setPosGrid.z - m_minZ] = 1;
            GameObject obj = Instantiate(m_tank, setPos, Quaternion.identity);
            obj.transform.parent = transform;
        }
        else
        {
            Debug.Log("すでに設置済みです。ここには設置できません。");
        }
    }

    /// <summary>
    /// 取得した戦車の設置位置のVector3Intの情報をVector3に変換する
    /// </summary>
    /// <param name="tankPosToGrid"></param>
    /// <returns></returns>
    private Vector3 TankSetPosition(Vector3Int tankPosToGrid)
    {
        int x = tankPosToGrid.x;
        int z = tankPosToGrid.z;
        Vector3 tankSetPos = new Vector3(x, 0.3f, z);　// 戦車の設置の高さ（0.3f）;
        return tankSetPos;
    }

    /// <summary>
    /// Markerのポジションを移動する
    /// </summary>
    /// <param name="hitPos"></param>
    void MarkerAction(Vector3 hitPos)
    {
        if (hitPos.x >= m_minX && hitPos.x <= m_maxX)
        {
            if (hitPos.z >= m_minZ && hitPos.z <= m_maxZ)
            {
                m_targetMarker.SetActive(true);
                Vector3Int setPosGrid = VectorInfo.TankSetPositionToGrid(hitPos);
                Vector3 markerPos = new Vector3(setPosGrid.x, 0.0001f, setPosGrid.z); // TargetMarkerの座標
                m_targetMarker.transform.position = markerPos;
            }
        }
        else
        {
            m_targetMarker.SetActive(false);
        }
        
    }
}
