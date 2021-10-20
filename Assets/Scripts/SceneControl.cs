using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public string NextScene;

    public void GotoNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
