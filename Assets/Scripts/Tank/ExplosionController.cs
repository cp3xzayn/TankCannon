using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    /// <summary> 爆発エフェクトを破棄するまでの時間 </summary>
    [SerializeField] float m_destroyTime = 2f;
    float m_timer;
    [SerializeField] AudioClip m_explosionAudio = null;
    /// <summary> AudioSouece </summary>
    AudioSource m_audio;
    /// <summary> 音量を保持しているSoundクラス </summary>
    Sound m_sound;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_audio.volume = Sound.SEVolume; //音量を設定
        m_audio.PlayOneShot(m_explosionAudio); //一度だけ効果音を流す
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
