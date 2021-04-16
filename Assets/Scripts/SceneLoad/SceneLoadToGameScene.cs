using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadToGameScene : MonoBehaviour
{
    public void OnClickStart()
    {
        FadeManager.Instance.LoadScene("GameScene", 0.3f);
    }
}
