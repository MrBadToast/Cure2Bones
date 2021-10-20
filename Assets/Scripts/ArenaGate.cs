using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGate : MonoBehaviour
{
   private void Start()
   {
      gameObject.SetActive(false);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == 7)
      {
         GetComponent<Collider>().enabled = false;
      }
   }
}
