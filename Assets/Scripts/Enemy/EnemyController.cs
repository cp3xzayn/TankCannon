using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /// <summary> 追従するオブジェクト </summary>
    GameObject m_target;
    /// <summary> スピード </summary>
    [SerializeField] float m_speed = 1f;
    /// <summary> 敵のHP </summary>
    int m_enemyHP;
    NavMeshAgent m_meshAgent;
    EnemyData m_enemyData;

    void Start()
    {
        m_enemyData = new EnemyData();
        m_enemyHP = m_enemyData.EnemyHP;
        m_meshAgent = GetComponent<NavMeshAgent>();
        m_target = GameObject.FindGameObjectWithTag("Player");
        m_meshAgent.velocity = Vector3.one * m_speed; //敵の移動速度を設定する
    }

    void Update()
    {
        m_meshAgent.destination = m_target.transform.position;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            m_enemyHP = m_enemyHP - 1;
            Debug.Log("敵のHPは" + m_enemyHP);
            if (m_enemyHP <= 0)
            {
                Destroy(this.gameObject);
            }
        }   
    }
}
