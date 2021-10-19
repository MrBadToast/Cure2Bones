using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float ObjectLifetime = 1.0f;
    public bool StartOnAwake = true;

    private void Start()
    {
        if(StartOnAwake)
        {
            StartCoroutine(RunTimer());
        }
    }

    public void StartTimer()
    {
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        yield return new WaitForSecondsRealtime(ObjectLifetime);
        Destroy(gameObject);
    }
}
