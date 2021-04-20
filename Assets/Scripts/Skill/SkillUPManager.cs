using UnityEngine;
using UnityEngine.UI;

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
    /// <summary> 生成間隔の上がり値(減らす) </summary>
    float m_shootTimeUP = -0.5f;
    /// <summary> 現在の弾生成間隔 </summary>
    float m_shootTime;

    /// <summary> 所持コスト </summary>
    [Header("所持コスト"), SerializeField] private int m_cost = 20;
    /// <summary> Wave終了時に得るコスト </summary>
    [Header("Wave終了時に得るコスト") ,SerializeField] private int m_getCost = 20;
    /// <summary> Wave終了時に得るコストを一度だけ足す </summary>
    bool isOneTimePulse = true;

    /// <summary> 所持コストを表示するTextのGameObject </summary>
    [SerializeField] GameObject m_costObj = null;
    /// <summary> 所持コストを表示するText </summary>
    Text m_costText;

    /// <summary> Skillの選択Button </summary>
    [SerializeField] Button[] m_skillButton = null;

    /// <summary> SkillData </summary>
    [SerializeField] SkillData m_skillData = null;

    void Start()
    {
        m_costText = m_costObj.GetComponent<Text>();
        // それぞれのステータスを初期化
        m_bulletDamage = TankStatus.BulletDamage;
        m_shootRange = TankStatus.ShootVelocity;
        m_shootTime = TankStatus.ShootTime;
    }

    void Update()
    {
        m_costText.text = "所持コスト:" + m_cost;
        ButtonInteractable();

        if (GameManager.Instance.NowGameState == GameState.End)
        {
            if (isOneTimePulse)
            {
                m_cost += m_getCost;
                isOneTimePulse = false;
            }
        }
        if (GameManager.Instance.NowGameState == GameState.Start) isOneTimePulse = true;
    }

    /// <summary>
    /// 所持コストとスキルを選択するのに必要なコストを比べてButtonのinteractableを決める
    /// </summary>
    void ButtonInteractable()
    {
        for (int i = 0; i < m_skillButton.Length; i++)
        {
            if (m_cost >= m_skillData.SkillParamList[i].m_needCost)
            {
                m_skillButton[i].interactable = true;
            }
            else
            {
                m_skillButton[i].interactable = false;
            }
        }
    }

    /// <summary> 射程Upが押されたときの処理 </summary>
    public void OnClickVelocityUP()
    {
        m_shootRange += m_shootRangeUP;
        TankStatus.ShootVelocity = m_shootRange;
        m_cost -= m_skillData.SkillParamList[0].m_needCost;
    }

    /// <summary> ダメージUpが押されたときの処理 </summary>
    public void OnClickDamageUP()
    {
        m_bulletDamage += m_bulletDamageUP;
        TankStatus.BulletDamage = m_bulletDamage;
        m_cost -= m_skillData.SkillParamList[1].m_needCost;
    }

    /// <summary> 生成間隔Upが押されたときの処理 </summary>
    public void OnClickShootTimeUP()
    {
        m_shootTime += m_shootTimeUP;
        TankStatus.ShootTime = m_shootTime;
        m_cost -= m_skillData.SkillParamList[2].m_needCost;
    }
}
