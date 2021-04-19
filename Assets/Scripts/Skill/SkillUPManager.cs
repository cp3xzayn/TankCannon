using UnityEngine;

public class SkillUPManager : MonoBehaviour
{
    /// <summary> ダメージUPの上がり値 </summary>
    int m_bulletDamageUP = 1;
    /// <summary> 現在の弾のダメージ </summary>
    int m_bulletDamage;
    /// <summary> 射程範囲の上がり値 </summary>
    float m_shootRangeUP = 10f;
    /// <summary> 現在の射程範囲 </summary>
    float m_shootRange;
    /// <summary> 生成間隔の上がり値(割る) </summary>
    int m_shootTimeUP = 2;
    /// <summary> 現在の弾生成間隔 </summary>
    float m_shootTime;

    void Start()
    {
        m_bulletDamage = TankStatus.BulletDamage;
        m_shootRange = TankStatus.ShootVelocity;
        m_shootTime = TankStatus.ShootTime;
    }

    /// <summary> ダメージUpが押されたときの処理 </summary>
    public void OnClickDamageUP()
    {
        m_bulletDamage += m_bulletDamageUP;
        TankStatus.BulletDamage = m_bulletDamage;
    }
    /// <summary> 射程Upが押されたときの処理 </summary>
    public void OnClickVelocityUP()
    {
        m_shootRange += m_shootRangeUP;
        TankStatus.ShootVelocity = m_shootRange;
    }
    /// <summary> 生成間隔Upが押されたときの処理 </summary>
    public void OnClickShootTimeUP()
    {
        m_shootTime /= m_shootTimeUP;
        TankStatus.ShootTime = m_shootTime;
    }
}
