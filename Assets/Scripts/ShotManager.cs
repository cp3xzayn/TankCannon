using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotManager : MonoBehaviour
{
    [SerializeField] GameObject m_shotObject = null;
    [SerializeField] GameObject m_shoter = null;
    [SerializeField] GameObject m_raycastObject = null;
    RaycastManager raycastManager;

    /// <summary> 一度のみ発射する </summary>
    private bool isOneShot = true;
    /// <summary> 一度のみ発射する </summary>
    public bool IsOneShot
    {
        set { isOneShot = value; }
    }

    [SerializeField] float m_shotAngle = 60.0f;
    void Start()
    {
        raycastManager = m_raycastObject.GetComponent<RaycastManager>();

    }
    void Update()
    {
        bool isShot = raycastManager.IsDecide;
        Vector3 targetPos = raycastManager.TargetPos;
        if (isShot)
        {
            if (isOneShot)
            {
                Shot(targetPos);
                isOneShot = false;
            }
        }
    }

    /// <summary>
    /// 弾を発射する
    /// </summary>
    /// <param name="targetPosition"></param>
    private void Shot(Vector3 targetPosition)
    {
        Vector3 targetPos = raycastManager.TargetPos;
        float iniVec = InitialVelocity(targetPos);

        Vector3 vec = ConvertToVector3(iniVec, targetPosition);
        InstantiateObject(vec);
    }

    private float InitialVelocity(Vector3 targetPosition)
    {
        Vector3 startPos = m_shotObject.transform.position;
        Vector3 targetPos = targetPosition;
        float distance = Vector3.Distance(targetPos, startPos);

        float x = distance;
        float g = Physics.gravity.y;
        float y0 = m_shotObject.transform.position.y;
        float y = targetPos.y;
        float rad = m_shotAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);

        float v0Square = g * x * x / (2 * cos * cos * (y - y0 - x * tan));
        float v0 = Mathf.Sqrt(v0Square);

        return v0;
    }

    private Vector3 ConvertToVector3(float v0, Vector3 targetPosition)
    {
        Vector3 startPos = m_shoter.transform.position;
        Vector3 targetPos = targetPosition;
        startPos.y = 0;
        targetPos.y = 0;

        Vector3 dir = (targetPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = v0 * Vector3.right;
        vec = yawRot * Quaternion.AngleAxis(m_shotAngle, Vector3.forward) * vec;
        return vec;
    }

    private void InstantiateObject(Vector3 shotVec)
    {
        GameObject obj = Instantiate(m_shotObject, m_shoter.transform.position, Quaternion.identity);
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        Vector3 force = shotVec * rb.mass;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
