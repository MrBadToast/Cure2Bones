using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamageArea : MonoBehaviour
{
    public List<TargetObject> targetsInReach;

    private void Awake()
    {
        targetsInReach = new List<TargetObject>();
    }

    public List<TargetObject> GetTargetsInReach()
    {
        return targetsInReach;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            TargetObject t;
            if (other.TryGetComponent(out t))
            {
                targetsInReach.Add(t);
            }
            else
            {
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            TargetObject t;
            if (other.TryGetComponent(out t))
            {
                targetsInReach.Remove(t);
            }
            else
            {
                return;
            }
        }
    }
    
}
