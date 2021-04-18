using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary> 爆発エフェクト </summary>
    GameObject m_effect;
    /// <summary> 弾を生成した戦車 </summary>
    GameObject m_tank;
    /// <summary> EnemyDetector </summary>
    EnemyDetector m_enemyDector;
    /// <summary> 最も近い敵のポジション </summary>
    Vector3 m_targetPos;

    void Start()
    {
        m_tank = this.transform.root.gameObject;
        m_effect = Resources.Load<GameObject>("Explosion");
        m_enemyDector = GetComponentInParent<EnemyDetector>();
        m_targetPos = m_enemyDector.Target.transform.position;
        this.gameObject.transform.LookAt(m_targetPos); // 敵の方向に弾を向ける
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("敵に衝突");
            GameObject obj = Instantiate(m_effect, this.transform.position, Quaternion.identity);
            obj.transform.parent = m_tank.transform; // 戦車の子オブジェクトにする
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Field")
        {
            Debug.Log("地面に衝突");
            GameObject obj = Instantiate(m_effect, this.transform.position, Quaternion.identity);
            obj.transform.parent = m_tank.transform; // 戦車の子オブジェクトにする
            Destroy(this.gameObject);
        }
    }
}
