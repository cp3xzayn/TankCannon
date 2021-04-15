using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    /// <summary> 弾のオブジェクト </summary>
    [SerializeField] GameObject m_shotObject = null;
    /// <summary> 弾を発射するオブジェクト </summary>
    [SerializeField] GameObject m_shoter = null;
    /// <summary> EnemyController </summary>
    [SerializeField] EnemyDetector m_enemyDector = null;
    /// <summary> 弾のスピード </summary>
    [SerializeField] float m_shootVelocity = 5f;

    /// <summary> 弾を一度のみ発射する </summary>
    private bool isOneShot = true;
    /// <summary> 弾の生成間隔 </summary>
    [SerializeField] float m_shotTime = 2.0f;
    float m_timer;

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Playing)
        {
            if (isOneShot)
            {
                Shot();
            }
            else
            {
                m_timer += Time.deltaTime;
                if (m_timer > m_shotTime)
                {
                    m_timer = 0;
                    isOneShot = true;
                }
            }
        }
    }

    /// <summary>
    /// 弾を発射する関数 
    /// </summary>
    void Shot()
    {
        if (m_enemyDector.Target != null)
        {
            Vector3 targetPos = m_enemyDector.Target.transform.position;
            Vector3 vec = GetVeCtor3ToTarget(targetPos);
            InstantiateBullet(vec);
            isOneShot = false;
        }
        else
        {
            Debug.Log("敵を検知できませんでした。");
        }
    }

    /// <summary>
    /// 弾を飛ばす方向を求める（戦車と敵から単位ベクトルを求め、力を掛けている）
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private Vector3 GetVeCtor3ToTarget(Vector3 targetPosition)
    {
        Vector3 startPos = m_shoter.transform.position;
        Vector3 targetPos = targetPosition;
        Vector3 shotVector = (targetPos - startPos).normalized * m_shootVelocity;

        return shotVector;
    }

    /// <summary>
    /// 弾を生成し、力を加える
    /// </summary>
    /// <param name="shootVec"></param>
    private void InstantiateBullet(Vector3 shootVec)
    {
        GameObject obj = Instantiate(m_shotObject, m_shoter.transform.position, Quaternion.identity);
        Rigidbody rb = obj.AddComponent<Rigidbody>();

        Vector3 force = shootVec * rb.mass;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
