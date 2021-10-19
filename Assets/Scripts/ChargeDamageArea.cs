using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDamageArea : MonoBehaviour
{
    private PlayerBehavior player;

    private void Start()
    {
        player = PlayerBehavior.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            var t = other.gameObject.GetComponent<TargetObject>();
            t.OnHit(new HitData(t.transform.position - transform.position, player.ChargePower, player.AttackPower * 8.0f, 3.0f));
            GetComponent<SoundModule_Base>().Play("hit");
        }
    }
}
