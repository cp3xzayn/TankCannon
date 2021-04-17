using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    /// <summary> 現在のWave </summary>
    private int m_wave = 1;
    /// <summary> 現在のWave数 </summary>
    public int Wave
    {
        set { m_wave = value; }
        get { return m_wave; }
    }
    /// <summary> WaveTextの表示時間 </summary>
    [SerializeField] float m_indicateTime = 2;
    /// <summary> Wave数を表示するGameObject </summary>
    [SerializeField] GameObject m_waveStartObj = null;
    /// <summary> 戦車の設置位置を決定するためのボタン </summary>
    [SerializeField] GameObject m_confirmButton = null;
    /// <summary> Wave数を表示するText </summary>
    Text m_waveStartText;
    [SerializeField] GameObject m_drawLineOnTankRange = null;

    void Start()
    {
        m_waveStartText = m_waveStartObj.GetComponent<Text>();
    }

    void Update()
    {
        DoInThisGameState();
    }

    /// <summary>
    /// GameStateに応じて処理をする関数
    /// </summary>
    void DoInThisGameState()
    {
        if (GameManager.Instance.NowGameState == GameState.Start) IndicateWave();
        if (GameManager.Instance.NowGameState == GameState.Prepare)
        {
            m_drawLineOnTankRange.SetActive(true);
            m_confirmButton.transform.localScale = Vector3.one;
        }
        else
        {
            m_drawLineOnTankRange.SetActive(false);
            m_confirmButton.transform.localScale = Vector3.zero;
        }
        if (GameManager.Instance.NowGameState == GameState.End)
        {
            DestoryEnemies();
            GoNextWave();
        }
    }

    /// <summary>
    /// Waveが次に進んだ時の処理
    /// </summary>
    public void GoNextWave()
    {
        m_wave++;
        GameManager.Instance.SetNowState(GameState.Start);
    }

    /// <summary>
    /// GameStateがEndになった時、Fieldに敵がいた場合見つけ、破壊する
    /// </summary>
    public void DestoryEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    /// <summary> Textで現在のWaveを表示する </summary>
    public void IndicateWave()
    {
        StartCoroutine("IndicateWaveText");
    }

    /// <summary>
    /// RoundStartのTextを数秒表示し、時間がたったら非表示にする
    /// </summary>
    /// <returns></returns>
    IEnumerator IndicateWaveText()
    {
        m_waveStartText.text = "Wave" + m_wave;
        m_waveStartObj.transform.localScale = Vector3.one; //Scaleを1にして表示する
        yield return new WaitForSeconds(m_indicateTime);
        m_waveStartObj.transform.localScale = Vector3.zero; //Scaleを0にして非表示にする
        GameManager.Instance.SetNowState(GameState.Prepare); //GameStateをPrepareにする
        yield break;
    }

    /// <summary>
    /// 準備フェーズからプレイフェーズに進む
    /// </summary>
    public void OnClickPlayingFromPrepare()
    {
        if (GameManager.Instance.NowGameState == GameState.Prepare)
        {
            GameManager.Instance.SetNowState(GameState.Playing);
        }
    }
}
