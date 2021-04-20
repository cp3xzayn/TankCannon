using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    /// <summary> BGMSliderのGameObject </summary>
    GameObject m_bGMSliderObj = null;
    /// <summary> SESliderのGameObject </summary>
    GameObject m_sESliderObj = null;
    /// <summary> BGMSlider </summary>
    Slider m_bGMSlider = null;
    /// <summary> SESlider </summary>
    Slider m_sESlider = null;
    /// <summary> SoundSetting画面を開くボタン </summary>
    Button m_soundSettingOpen = null;
    /// <summary> SoundSetting画面を閉じるボタン </summary>
    Button m_soundSettingClose = null;
    /// <summary> Startボタンなど、1シーンのみで必要なCanvas </summary>
    GameObject m_baseCanvas = null;
    AudioSource m_audioSource;

    void Start()
    {
        m_bGMSliderObj = FindChieldGameObject(this.gameObject, "BGMSlider");
        m_sESliderObj = FindChieldGameObject(this.gameObject, "SESlider");
        m_bGMSlider = m_bGMSliderObj.GetComponent<Slider>();
        m_sESlider = m_sESliderObj.GetComponent<Slider>();
        m_audioSource = GetComponent<AudioSource>();
        SubcribeAddListener(m_soundSettingOpen, "SoundSettingOpen", false);
        SubcribeAddListener(m_soundSettingClose, "SoundSettingClose", true);
        m_baseCanvas = GameObject.FindWithTag("BaseCanvas");
    }

    void Update()
    {
        SetBGMVolume();
        Sound.SEVolume = m_sESlider.value;
    }

    /// <summary> BGMの音量をセットする関数</summary>
    void SetBGMVolume()
    {
        Sound.BGMVolume = m_bGMSlider.value;
        m_audioSource.volume = Sound.BGMVolume;
    }


    /// <summary>
    /// アタッチされたGameObjectの子オブジェクトを検索し、指定したTagのGameObjectを返すメソッド
    /// </summary>
    /// <param name="obj">検索対象のゲームオブジェクト</param>
    /// <param name="tag">検索するためのTag</param>
    /// <param name="includeInactive">非アクティブのオブジェクトを検索する場合はtrueにする</param>
    /// <returns></returns>
    GameObject FindChieldGameObject(GameObject obj, string tag, bool includeInactive = false)
    {
        var children = obj.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            if (transform.gameObject.tag == tag)
            {
                return transform.gameObject;
            }
        }

        return null;
    }

    /*---------------------------------------------------------------------------------*/
    // 以下
    // BaseCanvasを非表示にしていたのは、2つ以上のCanvasを作っていたので、ぐちゃぐちゃにならないようにするため。
    // SortOrderでできる内容だった。→ SortOrderで実装済み

    /// <summary>
    /// Buttonに関数をAddListenerするメソッド
    /// </summary>
    /// <param name="button">AddListenerするButton</param>
    /// <param name="tag">検索するためのTag</param>
    /// <param name="isBaseCanvasOpen">BaseCanvasを表示するかしないか</param>
    void SubcribeAddListener(Button button, string tag, bool isBaseCanvasOpen)
    {
        button = FindChieldGameObject(this.gameObject, tag).GetComponent<Button>();
        button.onClick.AddListener(() => SoundSettingOpenOrClose(isBaseCanvasOpen));
    }

    /// <summary>
    /// SoundSettingが開いてるかどうかを判定し、BaseCanvasを非表示にする
    /// </summary>
    /// <param name="isBaseCanvasOpen"></param>
    void SoundSettingOpenOrClose(bool isBaseCanvasOpen)
    {
        if (m_baseCanvas != null)
        {
            m_baseCanvas.SetActive(isBaseCanvasOpen);
        }
    }

    /*---------------------------------------------------------------------------------*/
}
