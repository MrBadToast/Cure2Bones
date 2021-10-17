using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour
{
    public string sceneName;

    public void GotoNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
