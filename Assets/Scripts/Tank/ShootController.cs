using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戦車(Player)と弾の発射に関するクラス
/// </summary>
public class ShootController : MonoBehaviour
{
    /// <summary> 弾のオブジェクト </summary>
    GameObject m_shootObject = null;
    /// <summary> 弾を発射するオブジェクト </summary>
    [SerializeField] GameObject m_shooter = null;
    /// <summary> EnemyDetector </summary>
    [SerializeField] EnemyDetector m_enemyDector = null;
    /// <summary> 戦車の上の部分のGameObject</summary>
    [SerializeField] GameObject m_tankTower = null;
    /// <summary> 弾のスピード </summary>
    [SerializeField] float m_shootVelocity;
    /// <summary> 弾の生成間隔 </summary>
    [SerializeField] float m_shootTime;
    float m_timer;
    /// <summary> 弾を一度のみ発射する </summary>
    private bool isOneShoot = true;
    /// <summary> ステータスを一度だけ設定するためのbool </summary>
    private bool isOneTimeSet;
    

    void Start()
    {
        m_shootObject = Resources.Load<GameObject>("Bullet");
        // 各ステータスを初期化
        m_shootVelocity = TankStatus.ShootVelocity;
        m_shootTime = TankStatus.ShootTime;
    }

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Start) isOneTimeSet = true;
        if (GameManager.Instance.NowGameState == GameState.Prepare) SetTankStatusOneTime();
        if (GameManager.Instance.NowGameState == GameState.Playing)
        {
            if (isOneShoot)
            {
                Shot();
            }
            else
            {
                m_timer += Time.deltaTime;
                if (m_timer > m_shootTime)
                {
                    m_timer = 0;
                    isOneShoot = true;
                }
            }
        }
    }

    /// <summary>
    /// GameState.Prepareになったとき一度だけ各ステータスを設定する
    /// </summary>
    void SetTankStatusOneTime()
    {
        if (isOneTimeSet)
        {
            m_shootVelocity = TankStatus.ShootVelocity;
            m_shootTime = TankStatus.ShootTime;
            Debug.Log($"射程範囲{m_shootVelocity}");
            Debug.Log($"生成間隔{m_shootTime}");
            isOneTimeSet = false;
        }
    }

    /// <summary>
    /// 弾を発射する関数 
    /// </summary>
    void Shot()
    {
        if (m_enemyDector.Target != null)
        {
            Vector3 targetPos = m_enemyDector.Target.transform.position;
            m_tankTower.transform.LookAt(targetPos);// 戦車の上部を敵の方向に向ける
            Vector3 vec = GetVeCtor3ToTarget(targetPos);
            InstantiateBullet(vec);
            isOneShoot = false;
        }
        else
        {
            Debug.Log("敵を検知できませんでした。");
        }
    }

    /// <summary>
    /// 弾を飛ばす方向を求める（戦車と敵から単位ベクトルを求め、力を掛けている）
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private Vector3 GetVeCtor3ToTarget(Vector3 targetPosition)
    {
        Vector3 startPos = m_shooter.transform.position;
        Vector3 targetPos = targetPosition;
        Vector3 shotVector = (targetPos - startPos).normalized * m_shootVelocity;

        return shotVector;
    }

    /// <summary>
    /// 弾を生成し、力を加える
    /// </summary>
    /// <param name="shootVec"></param>
    private void InstantiateBullet(Vector3 shootVec)
    {
        GameObject obj = Instantiate(m_shootObject, m_shooter.transform.position, Quaternion.identity);
        obj.transform.parent = transform; //弾を戦車の子オブジェクトにする
        Rigidbody rb = obj.AddComponent<Rigidbody>();

        Vector3 force = shootVec * rb.mass;

        rb.AddForce(force, ForceMode.Impulse);
    }
}

/// <summary>
/// 戦車の情報を保持しているクラス
/// </summary>
public static class TankStatus
{
    /// <summary> 弾のダメージ </summary>
    static int m_bulletDamage = 1;
    /// <summary> 弾のダメージ </summary>
    public static int BulletDamage
    {
        set { m_bulletDamage = value; }
        get { return m_bulletDamage; }
    }

    /// <summary> 弾のスピード </summary>
    static float m_shootVelocity = 50f;
    /// <summary> 弾のスピード </summary>
    public static float ShootVelocity
    {
        set { m_shootVelocity = value; }
        get { return m_shootVelocity; }
    }
    /// <summary> 弾を生成する間隔 </summary>
    static float m_shootTime = 4;
    /// <summary> 弾を生成する間隔 </summary>
    public static float ShootTime
    {
        set { m_shootTime = value; }
        get { return m_shootTime; }
    }
}

