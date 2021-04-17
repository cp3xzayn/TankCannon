using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    /// <summary> スタートボタン </summary>
    [SerializeField] Button m_startButton = null;
    /// <summary> SceneLoadManager </summary>
    SceneLoadManager m_scene;
    void Start()
    {
        m_scene = FindObjectOfType<SceneLoadManager>();
        m_startButton.GetComponent<Button>().onClick.AddListener(() => m_scene.OnClickStart());
    }
}
