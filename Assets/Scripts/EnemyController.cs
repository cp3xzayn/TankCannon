using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject m_target;
    NavMeshAgent m_meshAgent;

    void Start()
    {
        m_meshAgent = GetComponent<NavMeshAgent>();
        m_target = GameObject.FindGameObjectWithTag("Player");
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
