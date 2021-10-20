using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject EsacpeMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!EsacpeMenu.activeInHierarchy)
                EsacpeMenu.SetActive(true);
        }
    }
}
