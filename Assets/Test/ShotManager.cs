using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 射法投射で弾を飛ばすためのクラス
/// </summary>
public class ShotManager : MonoBehaviour
{
    /// <summary> 弾のオブジェクト </summary>
    [SerializeField] GameObject m_shotObject = null;
    /// <summary> 弾を発射するオブジェクト </summary>
    [SerializeField] GameObject m_shoter = null;
    /// <summary> 弾の発射角度 </summary>
    [SerializeField] float m_shotAngle = 60.0f;

    /// <summary>
    /// 弾を発射する
    /// </summary>
    /// <param name="targetPosition"></param>
    private void Shot()
    {
        Vector3 targetPos = new Vector3(3, 0, 3); //ここにターゲットのポジションを入れる
        float iniVec = InitialVelocity(targetPos);
        Vector3 vec = ConvertToVector3(iniVec, targetPos);
        InstantiateObject(vec);
    }

    /// <summary>
    /// 初速の大きさを返す
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private float InitialVelocity(Vector3 targetPosition)
    {
        Vector3 startPos = m_shotObject.transform.position;
        Vector3 targetPos = targetPosition;
        float distance = Vector3.Distance(targetPos, startPos);

        float x = distance;
        float g = Physics.gravity.y; //重力加速度
        float y0 = m_shotObject.transform.position.y;
        float y = targetPos.y;
        float rad = m_shotAngle * Mathf.Deg2Rad; //角度をラジアンに変更
        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);
        // 初速の大きさを求めるための式
        float v0Square = g * x * x / (2 * cos * cos * (y - y0 - x * tan));
        float v0 = Mathf.Sqrt(v0Square);

        return v0;
    }

    /// <summary>
    /// 力を加える方向のベクトルを求め、返す
    /// </summary>
    /// <param name="v0"></param>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 弾のオブジェクトを生成し、力を加える
    /// </summary>
    /// <param name="shotVec"></param>
    private void InstantiateObject(Vector3 shotVec)
    {
        GameObject obj = Instantiate(m_shotObject, m_shoter.transform.position, Quaternion.identity);
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        
        Vector3 force = shotVec * rb.mass;

        rb.AddForce(force, ForceMode.Impulse);
    }

}
