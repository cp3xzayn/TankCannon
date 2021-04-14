using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGenerator : MonoBehaviour
{
    /// <summary> 生成する戦車のオブジェクト </summary>
    [SerializeField] GameObject m_tank = null;

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
    [SerializeField] int m_maxX = 5;
    [SerializeField] int m_minX = -5;
    [SerializeField] int m_maxY = -2;
    [SerializeField] int m_minY = -7;
    /*-----------------------------------------*/

    /// <summary>
    /// 戦車を生成する
    /// </summary>
    /// <param name="hitPos"></param>
    void InstantiateTank(Vector3 hitPos)
    {
        Vector3Int setPos = VectorInfo.TankSetPosition(hitPos);
        // 戦車が生成可能範囲が選択されたら
        if (hitPos.x > m_minX && hitPos.x < m_maxX && hitPos.z > m_minY && hitPos.z < m_maxY)
        {
            // 戦車設置可能かを判断し、可能なら戦車を生成する。
            // TODO:最終的にマジックナンバーをなくす
            if (VectorInfo.mapInfo[setPos.x + 4, setPos.z + 6] == 0)
            {
                VectorInfo.mapInfo[setPos.x + 4, setPos.z + 6] = 1;
                GameObject obj = Instantiate(m_tank, setPos, Quaternion.identity);
                obj.transform.parent = transform;
            }
            else
            {
                Debug.Log("すでに設置済みです。ここには設置できません。");
            }
        }
        else
        {
            Debug.Log("設置可能範囲外です。");
        }
    }
}
