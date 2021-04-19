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
    /// <summary> GameClearのWave数 </summary>
    [SerializeField] int m_maxWave = 5;
    /// <summary> WaveTextの表示時間 </summary>
    [SerializeField] float m_indicateTime = 2;

    /*----------------------------------------------------------------*/
    // GameObjectの設定
    /*----------------------------------------------------------------*/
    /// <summary> Wave数を表示するGameObject </summary>
    [SerializeField] GameObject m_waveStartObj = null;
    /// <summary> 戦車の設置位置を決定するためのボタン </summary>
    [SerializeField] GameObject m_confirmButton = null;
    /// <summary> Wave数を表示するText </summary>
    Text m_waveStartText;
    /// <summary> Skillを選択するPanelのGameObject </summary>
    [SerializeField] GameObject m_skillPanelObj = null;
    /// <summary> Skillを選択するPanelのAnimator </summary>
    Animator m_skillPanelAnimator;
    /// <summary> GameOverを表示するGameObject </summary>
    [SerializeField] GameObject m_gameOverTextObj = null;
    /// <summary> GameOverTextを表示するAnimator </summary>
    Animator m_gameOverAnimator;
    /// <summary> GameClearを表示するGameObject </summary>
    [SerializeField] GameObject m_gameClearTextObj = null;
    /// <summary> GameClearTextを表示するAnimator </summary>
    Animator m_gameClearAnimator;
    /// <summary> Titleに戻るボタン </summary>
    [SerializeField] GameObject m_backToTitleButtonObj = null;
    Button m_backToTitleButton;
    /// <summary> 戦車の移動可能範囲に線を引くGameObject </summary>
    [SerializeField] GameObject m_drawLineOnTankRange = null;


    /// <summary> Buttonに一度だけAddListenerするためのbool </summary>
    bool isOneTimeSet = true;
    /// <summary> SkillSetPanelのAnimatorを一度だけ再生するため </summary>
    bool isOneTimeAnimatorPlay = true;
    /// <summary> SceneLoadManager </summary>
    SceneLoadManager m_sceneLoadManager;

    void Start()
    {
        m_sceneLoadManager = FindObjectOfType<SceneLoadManager>();
        m_waveStartText = m_waveStartObj.GetComponent<Text>();
        m_skillPanelAnimator = m_skillPanelObj.GetComponent<Animator>();
        m_gameOverAnimator = m_gameOverTextObj.GetComponent<Animator>();
        m_gameClearAnimator = m_gameClearTextObj.GetComponent<Animator>();
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
        if (GameManager.Instance.NowGameState == GameState.SkillTime) GameStateSkillTime();

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
        if (GameManager.Instance.NowGameState == GameState.End) GameStateEnd();
        if (GameManager.Instance.NowGameState == GameState.GameOver) GameStateGameOver();
        if (GameManager.Instance.NowGameState == GameState.Finish) GameStateGameClear();
    }

    /// <summary>
    /// GameStateがSkillTimeの時の処理
    /// </summary>
    void GameStateSkillTime()
    {
        if (isOneTimeAnimatorPlay)
        {
            m_skillPanelAnimator.Play("ZoomIn");
            isOneTimeAnimatorPlay = false;
        }
    }

    /// <summary>
    /// GameStateがEndになったときの処理
    /// </summary>
    void GameStateEnd()
    {
        //Fieldに敵がいた場合見つけ、破壊する
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        // 現在のWave数に応じて処理変える
        if (m_wave < m_maxWave) GameManager.Instance.SetNowState(GameState.Start);
        else GameManager.Instance.SetNowState(GameState.Finish);
        m_wave++; // Waveを増やす
        isOneTimeAnimatorPlay = true;
    }

    /// <summary>
    /// GameStateがGameOverになったときの処理
    /// </summary>
    void GameStateGameOver()
    {
        m_gameOverAnimator.Play("ZoomIn");
        m_backToTitleButtonObj.SetActive(true);
        m_backToTitleButton = m_backToTitleButtonObj.GetComponent<Button>();
        if (isOneTimeSet)
        {
            m_backToTitleButton.onClick.AddListener(() => m_sceneLoadManager.OnClickToTitle());
            isOneTimeSet = false;
        }
    }

    /// <summary>
    /// GameStateがFinishになったときの処理
    /// </summary>
    void GameStateGameClear()
    {
        m_gameClearAnimator.Play("ZoomIn");
        m_backToTitleButtonObj.SetActive(true);
        m_backToTitleButton = m_backToTitleButtonObj.GetComponent<Button>();
        if (isOneTimeSet)
        {
            m_backToTitleButton.onClick.AddListener(() => m_sceneLoadManager.OnClickToTitle());
            isOneTimeSet = false;
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
        GameManager.Instance.SetNowState(GameState.SkillTime); //GameStateをPrepareにする
        yield break;
    }

    /*--------------------------------------------------------------------*/
    // ↓以下ボタンに設定している関数
    /*--------------------------------------------------------------------*/

    /// <summary>
    /// スキル画面が閉じられた時の処理
    /// </summary>
    public void OnClickCloseSkill()
    {
        GameManager.Instance.SetNowState(GameState.Prepare);
    }

    /// <summary>
    /// 準備フェーズからプレイPhaseに進む
    /// </summary>
    public void OnClickPlayingFromPrepare()
    {
        if (GameManager.Instance.NowGameState == GameState.Prepare)
        {
            GameManager.Instance.SetNowState(GameState.Playing);
        }
    }

}
