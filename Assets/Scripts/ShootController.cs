using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    /// <summary> 弾のオブジェクト </summary>
    [SerializeField] GameObject m_shotObject = null;
    /// <summary> 弾を発射するオブジェクト </summary>
    [SerializeField] GameObject m_shoter = null;
    /// <summary> TankController </summary>
    [SerializeField] TankController m_tankController = null;
    /// <summary> EnemyController </summary>
    [SerializeField] EnemyDetector m_enemyDector = null;

    [SerializeField] float m_shootVelocity = 5f;


    /// <summary> 一度のみ発射する </summary>
    private bool isOneShot = true;

    [SerializeField] float m_shotTime = 2.0f;
    float m_timer;

    void Update()
    {
        if (m_tankController.Direction == Vector3.zero)
        {
            if (isOneShot)
            {
                Shot();
            }
            if (!isOneShot)
            {
                m_timer += Time.deltaTime;
                if (m_timer > m_shotTime)
                {
                    m_timer = 0;
                    isOneShot = true;
                }
            }
        }
    }

    void Shot()
    {
        if (m_enemyDector.Target != null)
        {
            Vector3 targetPos = m_enemyDector.Target.transform.position;
            Vector3 vec = GetVeCtor3ToTarget(targetPos);
            InstantiateBullet(vec);
            isOneShot = false;
        }
        else
        {
            Debug.Log("敵を検知できませんでした。");
        }
    }

    private Vector3 GetVeCtor3ToTarget(Vector3 targetPosition)
    {
        Vector3 startPos = m_shoter.transform.position;
        Vector3 targetPos = targetPosition;

        Vector3 shotVector = (targetPos - startPos).normalized * m_shootVelocity;

        return shotVector;
    }

    private void InstantiateBullet(Vector3 shootVec)
    {
        GameObject obj = Instantiate(m_shotObject, m_shoter.transform.position, Quaternion.identity);
        Rigidbody rb = obj.AddComponent<Rigidbody>();

        Vector3 force = shootVec * rb.mass;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
