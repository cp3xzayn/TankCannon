using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenerator : MonoBehaviour
{
    /// <summary> 生成する戦車のオブジェクト </summary>
    [SerializeField] GameObject[] m_tank = null;
    /// <summary> オブジェクトをつかんでいるか判定する </summary>
    bool isGrabbing;
    /// <summary> つかんでいる戦車がどれか判定するための変数 </summary>
    int m_tankIndex;

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Prepare)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Tank")
                    {
                        if (hit.collider.gameObject == m_tank[0]) m_tankIndex = 0;
                        if (hit.collider.gameObject == m_tank[1]) m_tankIndex = 1;
                        if (hit.collider.gameObject == m_tank[2]) m_tankIndex = 2;
                        Debug.Log("戦車をつかんでいます。");
                        isGrabbing = true;
                    }
                }
            }
        }

        if (isGrabbing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Field")
                {
                    m_tank[m_tankIndex].transform.position = TankSetPosition(hit.point);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isGrabbing = false;
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
    /// 戦車の位置を一度Vector3Intに変換し、設置するためにもう一度Vector3に変換する
    /// </summary>
    /// <param name="tankPosToGrid"></param>
    /// <returns></returns>
    private Vector3 TankSetPosition(Vector3 raycastPos)
    {
        // GridポジションをVector3Intで取得する。
        Vector3Int tankPosToGrid = VectorInfo.TankSetPositionToGrid(raycastPos);
        // 高さが必要なため、もう一度Vector3に戻す
        int x = tankPosToGrid.x;
        int z = tankPosToGrid.z;
        Vector3 tankSetPos = new Vector3(x, 0.23f, z);　// 戦車の設置の高さ（0.23f）;
        return tankSetPos;
    }
}
