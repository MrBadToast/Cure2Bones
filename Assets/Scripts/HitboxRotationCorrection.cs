using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRotationCorrection : MonoBehaviour
{
    [SerializeField ]private Transform targetRotation;

    void Update()
    {
        transform.rotation = targetRotation.rotation;
    }
}
