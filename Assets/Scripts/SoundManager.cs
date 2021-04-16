using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    /// <summary> BGMSliderのGameObject </summary>
    GameObject m_bGMSliderObj = null;
    /// <summary> SESliderのGameObject </summary>
    GameObject m_sESliderObj = null;
    /// <summary> BGMSlider </summary>
    Slider m_bGMSlider;
    /// <summary> SESlider </summary>
    Slider m_sESlider;
    AudioSource m_audioSource;

    void Start()
    {
        m_bGMSliderObj = FindChieldGameObject(this.gameObject, "BGMSlider");
        m_sESliderObj = FindChieldGameObject(this.gameObject, "SESlider");
        m_bGMSlider = m_bGMSliderObj.GetComponent<Slider>();
        m_sESlider = m_sESliderObj.GetComponent<Slider>();
        m_audioSource = GetComponent<AudioSource>();
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
    /// <param name="tag">検索したいtag</param>
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
}
