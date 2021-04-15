using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenerator : MonoBehaviour
{
    /// <summary> 生成する戦車のオブジェクト </summary>
    [SerializeField] GameObject[] m_tank = null;
    /// <summary> TankInfoクラス </summary>
    [SerializeField] TankInfo m_tankInfo = null;
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
                if (hit.collider.tag == "Field")　// FieldにRayが当たったとき
                {
                    m_tank[m_tankIndex].transform.position = TankSetPosition(hit.point); // 戦車を移動する
                }
                if (Input.GetMouseButtonUp(0)) // 右クリックを離したら
                {
                    isGrabbing = false;
                }
            }
        }
    }

    /// <summary>
    /// 戦車の位置を一度Vector3Intに変換し、設置するためにもう一度Vector3に変換する
    /// </summary>
    /// <param name="tankPosToGrid"></param>
    /// <returns></returns>
    private Vector3 TankSetPosition(Vector3 raycastPos)
    {
        // GridポジションをVector3Intで取得する。
        Vector3Int tankPosToGrid = VectorInfo.TankSetPositionToGrid(raycastPos);
        // 戦車の移動可能範囲を制限する
        int x = Mathf.Clamp(tankPosToGrid.x, m_tankInfo.m_minX, m_tankInfo.m_maxX);
        int z = Mathf.Clamp(tankPosToGrid.z, m_tankInfo.m_minZ, m_tankInfo.m_maxZ);
        // 高さが必要なため、もう一度Vector3に戻す
        Vector3 tankSetPos = new Vector3(x, 0.23f, z);　// 戦車の設置の高さ（0.23f）;
        return tankSetPos;
    }
}
