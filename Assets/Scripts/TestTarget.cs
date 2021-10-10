using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : TargetObject
{
    [SerializeField] private Material red;
    
    
    public override void OnHit()
    {
        GetComponent<MeshRenderer>().material = red;
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 5f,ForceMode.Impulse);
    }
}
