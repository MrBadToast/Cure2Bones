using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickQuit : MonoBehaviour
{
    private float quitTime = 2f;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            quitTime -= Time.deltaTime;
            if (quitTime < 0)
                Application.Quit();
        }
        else
        {
            quitTime = 1f;
        }

        if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.H))
        {
            PlayerBehavior.Instance.GetMoney(1000);
        }
    }
}
