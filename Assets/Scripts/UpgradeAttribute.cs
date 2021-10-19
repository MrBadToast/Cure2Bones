using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAttribute : MonoBehaviour
{
    [SerializeField] private string UpgradeId;
    [SerializeField] private int InitialCost;
    [SerializeField] private int MaxLevel;
    [SerializeField] private float ValuePerLevel;
    [SerializeField] private bool IsMultiplyValue;
    [Space(20f)]
    [SerializeField] private Text CostText;
    [SerializeField] private Text LevelText;
    [SerializeField] private Image CostLabel;
    private DOTweenAnimation animation;

    private int currentCost;
    private int currentLevel;
    private float currentValue;

    private void Awake()
    {
        animation = GetComponent<DOTweenAnimation>();
    }

    private void Start()
    {
        currentCost = InitialCost;
        currentLevel = 1;
        if (IsMultiplyValue) currentValue = 1f;
        else currentValue = 0f;
    }

    public void OnTryUpgrade()
    {
        if (IsUpgradeAvailable())
        {
            PlayerBehavior.Instance.UseMoney(currentCost);
            currentLevel++;
            currentValue += ValuePerLevel;
            currentCost = (int)(currentCost * Mathf.Pow(currentLevel, 1.25f));
            CharacterUpgradeData.Instance.SetValue(UpgradeId, currentValue);
            UpdateButtonGrapics();
            animation.DORestart();
        }
    }

    public bool IsUpgradeAvailable()
    {
        if (PlayerBehavior.Instance == null) return false;
        
        if (PlayerBehavior.Instance.Money >= currentCost && currentLevel < MaxLevel)
            return true;
        
        return false;
    }

    public void UpdateButtonGrapics()
    {
        CostText.text = currentCost + " $ ";
        LevelText.text = "Lv." + currentLevel;
        if (IsUpgradeAvailable())
        {
            CostLabel.color = new Color(0.25f,0.83f,0f);
        }
        else
        {
            CostLabel.color = new Color(1f,0.53f,0f);
            if (MaxLevel == currentLevel)
            {
                CostLabel.color = new Color(0.6f,0.6f,0.6f);
                CostText.text = " -- ";
                LevelText.text = "Lv.MAX";
            }
        }
    }
}
