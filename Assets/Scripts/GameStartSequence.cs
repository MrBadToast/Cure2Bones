using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameStartSequence : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation anim_countdownGroup;
    [SerializeField] private DOTweenAnimation anim_countdownPanel;
    [SerializeField] private DOTweenAnimation anim_countdownText;
    [SerializeField] private DOTweenAnimation anim_gameplayUI;

    [SerializeField] private Text countdownText;
    
    public void StartSequence()
    {
        StartCoroutine(Cor_Sequence());
    }

    IEnumerator Cor_Sequence()
    {
        anim_countdownPanel.DOPlayById("COUNTDOWNPAN_IN");
        anim_countdownGroup.DOPlayById("COUNTDOWN_IN");
        
        countdownText.text = "3";
        anim_countdownText.DOPlay();

        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        anim_countdownText.DORestart();
        
        yield return new WaitForSeconds(1f);
        
        countdownText.text = "1";
        anim_countdownText.DORestart();
        
        yield return new WaitForSeconds(1f);
        
        countdownText.text = "GO";
        anim_countdownText.DORestart();
        
        yield return new WaitForSeconds(0.5f);
        anim_countdownGroup.DOPlayById("COUNTDOWN_OUT");
        anim_gameplayUI.DOPlayById("UI_FADEIN");
        
        StageManager.Instance.StartStage();
    }
}
