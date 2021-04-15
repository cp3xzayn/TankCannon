using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    /// <summary> 敵のGameObject </summary>
    [SerializeField] GameObject m_enemy = null;
    /// <summary> 敵の生成ポジションの配列 </summary>
    [SerializeField] GameObject[] m_generatePoint = null;
    /// <summary> 敵の生成間隔 </summary>
    [SerializeField] float m_enemyGenerateTime = 3f;
    /// <summary> 1Roundの制限時間 </summary>
    [SerializeField] float m_roundTime = 30f;
    /// <summary> 敵生成のコルーチンを一度だけ始めさせる </summary>
    private bool isGenerateStart = true;
    float m_timer;

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Playing)
        {
            if (isGenerateStart)
            {
                StartCoroutine("GenerateEnemy");
                isGenerateStart = false;
            }
            m_timer += Time.deltaTime;
            if (m_timer > m_roundTime)
            {
                Debug.Log("Round終了");
                StopCoroutine("GenerateEnemy");
                GameManager.Instance.SetNowState(GameState.End);
                isGenerateStart = true;
                m_timer = 0;
            }
        }
    }

    /// <summary>
    /// ランダムで敵の生成ポジションを決める(Indexを決める)
    /// </summary>
    /// <returns></returns>
    private int IndexFromRandom()
    {
        int generatePointIndex = Random.Range(0, m_generatePoint.Length);
        return generatePointIndex;
    }

    /// <summary>
    /// 敵をアタッチしているオブジェクトの子オブジェクトとして生成する
    /// </summary>
    void InstantiateEnemy()
    {
        GameObject obj = Instantiate(m_enemy, m_generatePoint[IndexFromRandom()].transform.position, Quaternion.identity);
        obj.transform.parent = transform;
    }

    /// <summary>
    /// 敵を一定間隔で生成するコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateEnemy()
    {
        while (true)
        {
            InstantiateEnemy();
            yield return new WaitForSeconds(m_enemyGenerateTime);
        }
    }
}
