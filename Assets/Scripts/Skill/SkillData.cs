using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/CreateSkillDataAsset")]
public class SkillData : ScriptableObject
{
    public List<SkillParam> SkillParamList = new List<SkillParam>();
}

/// <summary>
/// スキルの情報を保持するクラス
/// </summary>
[System.Serializable]
public class SkillParam
{
    /// <summary> 効果の名前 </summary>
    [Header("効果の名前")] public string m_skillName;
    /// <summary> 効果の画像 </summary>
    [Header("効果の画像")] public Sprite m_skillImage;
    /// <summary> 効果を得るために必要なコスト </summary>
    [Header("必要コスト")] public int m_needCost;
}
