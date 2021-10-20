using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSounds : MonoBehaviour
{
    private static GenericSounds _instace;

    public static GenericSounds Instace => _instace;

    private void Awake()
    {
        if (_instace == null)
            _instace = this;
        else
            Destroy(gameObject);
    }

    public void Play(string key)
    {
        GetComponent<SoundModule_Base>().Play(key);
    }
}
