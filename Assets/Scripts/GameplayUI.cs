using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Image Crosshair;
    [Space(20)] 
    [SerializeField] private Sprite CrosshairActive;
    [SerializeField] private Sprite CrosshairDeactive;
    
    private PlayerBehavior player;
    private bool _isplayerNull;

    private void Start()
    {
        player = PlayerBehavior.Instance;
        _isplayerNull = player == null;
    }

    private void Update()
    {
        if(_isplayerNull) return;
        
        healthText.text = player.CurrentHp.ToString() + " / " + player.MAXHp.ToString();
        staminaText.text = player.CurrentStm.ToString() + " / " + player.MAXStm.ToString();
        //moneyText.text = ;
        if (player.IsTargetInRange())
        {
            Crosshair.sprite = CrosshairActive;
        }
        else
        {
            Crosshair.sprite = CrosshairDeactive;
        }
        
    }
    
}
