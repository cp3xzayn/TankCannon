using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    /// <summary> BGMSliderのGameObject </summary>
    [SerializeField] GameObject m_bGMSliderObj = null;
    /// <summary> SESliderのGameObject </summary>
    [SerializeField] GameObject m_sESliderObj = null;
    /// <summary> BGMSlider </summary>
    [SerializeField] Slider m_bGMSlider = null;
    /// <summary> SESlider </summary>
    [SerializeField] Slider m_sESlider = null;
    AudioSource m_audioSource;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);    
    }

    void Start()
    {
        m_bGMSlider = m_bGMSliderObj.GetComponent<Slider>();
        m_sESlider = m_sESliderObj.GetComponent<Slider>();
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        SetBGMVolume();
        Sound.SEVolume = m_sESlider.value;
    }

    /// <summary> BGMの音量をセットする関数 </summary>
    void SetBGMVolume()
    {
        Sound.BGMVolume = m_bGMSlider.value;
        m_audioSource.volume = Sound.BGMVolume;
    }
}
