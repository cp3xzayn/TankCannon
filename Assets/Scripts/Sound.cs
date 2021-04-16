using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound 
{
    private static float m_bGMVolume;
    private static float m_sEVolume;

    public static float BGMVolume
    {
        set { m_bGMVolume = value; }
        get { return m_bGMVolume; }
    }

    public static float SEVolume
    {
        set { m_sEVolume = value; }
        get { return m_sEVolume; }
    }
}
