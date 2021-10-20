using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Image Crosshair;
    [SerializeField] private GameObject clearAnim;
    [SerializeField] private GameObject waveAnim;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject interacton;
    [Space(20)] 
    [SerializeField] private Sprite CrosshairActive;
    [SerializeField] private Sprite CrosshairDeactive;
    
    private PlayerBehavior player;
    private bool _isplayerNull;

    private void Start()
    {
        player = PlayerBehavior.Instance;
        _isplayerNull = player == null;
       // player.propertiesChanged += OnUpdateUI;
    }
    
    private void FixedUpdate()
    {
        if(_isplayerNull) return;
        
        healthText.text = ((int)player.CurrentHp).ToString() + " / " + player.MAXHp.ToString();
        staminaText.text = ((int)player.CurrentStm).ToString() + " / " + player.MAXStm.ToString();
        moneyText.text = (player.Money).ToString() + " $ ";
        
        if (player.IsTargetInRange())
        {
            Crosshair.sprite = CrosshairActive;
        }
        else
        {
            Crosshair.sprite = CrosshairDeactive;
        }

        InteractableOn(player.IsHeadingInteractables());

    }

    public void OnWaveClear(int waveNumber)
    {
        waveText.text = "Wave" + waveNumber + " Clear";
        waveAnim.GetComponent<DOTweenAnimation>().DORestartAllById("WAVECLEAR");
    }
    
    public void OnClear()
    {
        clearAnim.GetComponent<DOTweenAnimation>().DORestartAllById("CLEAR");
    }

    public void InteractableOn(bool value)
    {
        if (value)
        {
            interacton.SetActive(true);
        }
        else
        {
            interacton.SetActive(false);
        }
    }
    
    public void CharacterStaminaAlert()
    {
        
    }
    
    // private void OnUpdateUI()
    // {
    //     healthText.text = ((int)player.CurrentHp).ToString() + " / " + player.MAXHp.ToString();
    //     staminaText.text = ((int)player.CurrentStm).ToString() + " / " + player.MAXStm.ToString();
    //     moneyText.text = (player.Money).ToString() + " $ ";
    //     if (player.IsTargetInRange())
    //         Crosshair.sprite = CrosshairActive;
    //     else
    //         Crosshair.sprite = CrosshairDeactive;
    // }
    
}
