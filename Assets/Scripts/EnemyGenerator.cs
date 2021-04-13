using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_enemy = null;
    [SerializeField] GameObject[] m_generatePoint = null;

    void Start()
    {
        InstantiateEnemy();
    }

    void InstantiateEnemy()
    {
        GameObject obj = Instantiate(m_enemy, m_generatePoint[0].transform.position, Quaternion.identity);
        obj.transform.parent = transform;
    }
}
