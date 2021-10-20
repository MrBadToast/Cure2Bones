using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public virtual void GameStarted() {}
    public virtual void OnHit(HitData hitData) { }

    public virtual void Start()
    {
        //StageManager.Instance.onGameStarted += GameStarted;
    }

}
