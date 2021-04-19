using UnityEngine;
using UnityEngine.UI;

public class SkillDataSet : MonoBehaviour
{
    /// <summary> SkillDataのScriptableObject </summary>
    [SerializeField] SkillData m_skillData = null;

    /// <summary> Skillの名前のText </summary>
    [SerializeField] Text[] m_effectNameText = null;
    /// <summary> Skillの画像 </summary>
    [SerializeField] Image[] m_effectImage = null;
    /// <summary> 必要コストを表示するText </summary>
    [SerializeField] Text[] m_needCost = null;


    void Start()
    {
        SetSkillData();
    }

    /// <summary>
    /// Skillのデータをセットする関数
    /// </summary>
    void SetSkillData()
    {
        for (int i = 0; i < m_skillData.SkillParamList.Count; i++)
        {
            m_effectNameText[i].text = m_skillData.SkillParamList[i].m_skillName;
            m_effectImage[i].sprite = m_skillData.SkillParamList[i].m_skillImage;
            m_needCost[i].text = "必要コスト:" + m_skillData.SkillParamList[i].m_needCost;
        }
    }
}
