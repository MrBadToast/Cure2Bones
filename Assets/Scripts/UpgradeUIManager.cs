using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    private static UpgradeUIManager _instance;
    public static UpgradeUIManager Instance => _instance;

    [SerializeField] private UpgradeAttribute[] _upgradeAttributes;
    [SerializeField] private GameObject UIObject;

    public delegate void ONUpdateGrapics();

    public ONUpdateGrapics OnUpdateGraphics;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null) _instance = this;
        else Destroy(gameObject);

        foreach (var atr in _upgradeAttributes)
        {
            OnUpdateGraphics += atr.UpdateButtonGrapics;
        }
    }

    private bool flag = false;

    private void Start()
    {
        OnUpdateGraphics();
        UIObject.SetActive(false);
    }

    public void EnableUpgradeUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        PlayerBehavior.Instance.DisablePlayer();
        
        UIObject.SetActive(true);
        UIObject.GetComponent<DOTweenAnimation>().DORestartById("UPGRADE_ENABLE");
        UIObject.GetComponent<CanvasGroup>().interactable = true;
        
        OnUpdateGraphics();
    }

    public void DisableUpgradeUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        PlayerBehavior.Instance.EnablePlayer();
        
        UIObject.GetComponent<CanvasGroup>().interactable = false;
        UIObject.SetActive(false);
    }
    
}
