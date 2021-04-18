using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    /// <summary> 爆発エフェクトを破棄するまでの時間 </summary>
    [SerializeField] float m_destroyTime = 2f;
    float m_timer;

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
