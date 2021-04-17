using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private int m_enemyBulletDamage = 1;
    public int EnemyBulletDamage => m_enemyBulletDamage;
    /// <summary> BaseHPManager </summary>
    BaseHPManager m_baseHPManager;

    void Start()
    {
        m_baseHPManager = FindObjectOfType<BaseHPManager>();
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_baseHPManager.DecreaseBaseHp(m_enemyBulletDamage);
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Field") Destroy(this.gameObject);
    }
}
