using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject m_effect;

    void Start()
    {
        m_effect = Resources.Load<GameObject>("Explosion");
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("敵に衝突");
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Field")
        {
            Debug.Log("地面に衝突");
            Instantiate(m_effect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
