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
    NavMeshAgent m_meshAgent;

    void Start()
    {
        m_meshAgent = GetComponent<NavMeshAgent>();
        m_target = GameObject.FindGameObjectWithTag("Player");
        m_meshAgent.velocity = Vector3.one * m_speed;
    }

    void Update()
    {
        m_meshAgent.destination = m_target.transform.position;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }   
    }
}
