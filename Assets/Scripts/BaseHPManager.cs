using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHPManager : MonoBehaviour
{
    /// <summary> 拠点の耐久値 </summary>
    int m_baseHP = 10;
    /// <summary> 拠点の耐久値を表示するGameObject </summary>
    [SerializeField] GameObject m_baseHPSliderObj = null;
    /// <summary> 拠点の耐久値を表示するSlider </summary>
    Slider m_baseHPSlider;
    /// <summary> GameOverのテスト用 </summary>
    [SerializeField] bool isGameOver = false;

    void Start()
    {
        m_baseHPSlider = m_baseHPSliderObj.GetComponent<Slider>();
        m_baseHPSlider.maxValue = m_baseHP;
    }

    void Update()
    {
        m_baseHPSlider.value = m_baseHP;
        GameOverFromBaseHPZero();
        //テスト用
        if (isGameOver)
        {
            GameManager.Instance.SetNowState(GameState.GameOver);
        }
    }

    /// <summary>
    /// 弾が当たったときに拠点の耐久値を減らす
    /// </summary>
    /// <param name="eneBulletDamage"></param>
    public void DecreaseBaseHp(int eneBulletDamage)
    {
        m_baseHP -= eneBulletDamage;
        Debug.Log($"拠点の耐久値{m_baseHP}");
    }

    /// <summary>
    /// 拠点の耐久値が0になったときの処理
    /// </summary>
    void GameOverFromBaseHPZero()
    {
        if (m_baseHP <= 0)
        {
            Debug.Log("拠点の耐久値が0になりました。GameOver");
            GameManager.Instance.SetNowState(GameState.GameOver);
        }
    }
}
