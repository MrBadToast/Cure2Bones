using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    private static UpgradeUIManager _instance;
    public static UpgradeUIManager Instance => _instance;

    private UpgradeAttribute[] _upgradeAttributes;
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

    public void EnableUpgradeUI()
    {
        UIObject.SetActive(true);
        UIObject.GetComponent<DOTweenAnimation>().DORestartById("UPGRADE_ENABLE");
        UIObject.GetComponent<CanvasGroup>().interactable = true;
        OnUpdateGraphics();
    }

    public void DisableUpgradeUI()
    {
        UIObject.GetComponent<DOTweenAnimation>().DORestartById("UPGRADE_DISABLE");
        UIObject.GetComponent<CanvasGroup>().interactable = false;
        StartCoroutine(Cor_DisableUI());
    }

    IEnumerator Cor_DisableUI()
    {
        yield return new WaitForSeconds(1.0f);
        UIObject.SetActive(false);
    }
}
