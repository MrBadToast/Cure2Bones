using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ArenaGate : MonoBehaviour
{
   public DOTweenAnimation transition;
   public SceneControl sceneControl;
   private void Start()
   {
      gameObject.SetActive(false);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == 7)
      {
         GetComponent<Collider>().enabled = false;
         PlayerBehavior.Instance.DisablePlayer();
         transition.DOPlayById("FADEOUT");
         StopAllCoroutines();
         StartCoroutine(DelayedScenecontrol());
      }
   }

   IEnumerator DelayedScenecontrol()
   {
      yield return new WaitForSeconds(1.0f);
      sceneControl.GotoNextScene();
   }
}
