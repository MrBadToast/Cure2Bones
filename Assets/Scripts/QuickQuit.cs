using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickQuit : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.H))
        {
            PlayerBehavior.Instance.GetMoney(1000);
        }
    }
}
