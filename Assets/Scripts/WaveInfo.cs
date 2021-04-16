using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/CreateWaveInfoAsset")]
public class WaveInfo : ScriptableObject
{
    [Header ("現在のWave")]
    public int m_wave = 1;
    [Header("敵のスピード(indexはWave)")]
    public float[] m_enemySpeed;
}
