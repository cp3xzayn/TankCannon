using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /// <summary> 追従するオブジェクト </summary>
    GameObject m_target;
    /// <summary> スピード </summary>
    float m_speed;
    /// <summary> 敵のHP </summary>
    int m_enemyHP;
    NavMeshAgent m_meshAgent;
    /// <summary> EnemyDataのScriptableオブジェクト </summary>
    [SerializeField] EnemyData m_enemyData = null;
    /// <summary> 弾を生成するゲームオブジェクト </summary>
    [SerializeField] GameObject m_shoter = null;
    /// <summary> 弾のオブジェクト </summary>
    GameObject m_shotObject = null;
    /// <summary> 弾のスピード </summary>
    [SerializeField] float m_shotVelocity = 5f;
    /// <summary> 弾の生成をする範囲 </summary>
    [SerializeField] float m_shotableRange = 5f;
    /// <summary> 弾の生成クールタイム </summary>
    [SerializeField] float m_shotIntervalTime = 3f;
    float m_timer;

    Vector3 m_targetPos;

    void Start()
    {
        m_meshAgent = GetComponent<NavMeshAgent>();
        m_target = GameObject.FindGameObjectWithTag("Player");
        m_shotObject = Resources.Load<GameObject>("EnemyBullet");

        m_enemyHP = m_enemyData.m_enemyHP[0];
        m_speed = m_enemyData.m_enemySpeed[0];
        m_targetPos = m_target.transform.position;
        m_meshAgent.velocity = Vector3.one * m_speed; //敵の移動速度を設定する
    }

    void Update()
    {
        m_meshAgent.destination = m_target.transform.position;
        ShootToBase();
        StopEnemyMove();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            m_enemyHP = m_enemyHP - 1;
            if (m_enemyHP <= 0)
            {
                Destroy(this.gameObject);
            }
        }   
    }

    /// <summary>
    /// 敵が拠点との距離が範囲に入ったら止まるようにする。
    /// </summary>
    void StopEnemyMove()
    {
        float distance = DistanceFromEnemyToBase();
        if (distance <= m_shotableRange)
        {
            m_meshAgent.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// 拠点が範囲内にあるとき、弾を生成する
    /// </summary>
    void ShootToBase()
    {
        float distance = DistanceFromEnemyToBase();

        m_timer += Time.deltaTime;
        if (m_timer > m_shotIntervalTime)
        {
            if (distance <= m_shotableRange)
            {
                InstantiateBullet(GetVeCtor3ToTarget());
            }
            m_timer = 0;
        }
    }

    /// <summary>
    /// 敵と拠点の距離を計算する
    /// </summary>
    /// <returns></returns>
    float DistanceFromEnemyToBase()
    {
        Vector3 startPos = m_shoter.transform.position;
        m_targetPos = m_target.transform.position;
        float distance = Vector3.Distance(m_targetPos, startPos);

        return distance;
    }

    /// <summary>
    /// 弾を飛ばす方向を求める（敵と拠点から単位ベクトルを求め、力を掛けている）
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private Vector3 GetVeCtor3ToTarget()
    {
        Vector3 startPos = m_shoter.transform.position;
        m_targetPos = m_target.transform.position;
        Vector3 shootVector = (m_targetPos - startPos).normalized * m_shotVelocity;

        return shootVector;
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
