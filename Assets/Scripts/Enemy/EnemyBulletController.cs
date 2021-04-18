using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    /// <summary> 弾のダメージ </summary>
    private int m_enemyBulletDamage = 1;
    /// <summary> 弾のダメージ </summary>
    public int EnemyBulletDamage => m_enemyBulletDamage;
    /// <summary> 爆発エフェクト </summary>
    GameObject m_effect;
    /// <summary> BaseHPManager </summary>
    BaseHPManager m_baseHPManager;

    void Start()
    {
        m_effect = Resources.Load<GameObject>("Explosion");
        m_baseHPManager = FindObjectOfType<BaseHPManager>();
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_baseHPManager.DecreaseBaseHp(m_enemyBulletDamage);
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Field")
        {
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
