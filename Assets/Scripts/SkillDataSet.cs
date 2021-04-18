using UnityEngine;
using UnityEngine.UI;

public class SkillDataSet : MonoBehaviour
{
    [SerializeField] SkillData m_skillData = null;

    [SerializeField] Text[] m_effectNameText = null;
    [SerializeField] Image[] m_effectImage = null;
    [SerializeField] Text[] m_needCost = null;

    void Start()
    {
        SetSkillData();
    }

    void SetSkillData()
    {
        Debug.Log("q");
        for (int i = 0; i < m_skillData.SkillParamList.Count; i++)
        {
            Debug.Log("a");
            m_effectNameText[i].text = m_skillData.SkillParamList[i].m_skillName;
            m_effectImage[i].sprite = m_skillData.SkillParamList[i].m_skillImage;
            m_needCost[i].text = "必要コスト" + m_skillData.SkillParamList[i].m_needCost;
        }
    }
}
