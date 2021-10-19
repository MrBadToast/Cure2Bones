using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundModule_Base : MonoBehaviour
{
    protected AudioSource module_audsource;

    [Header("RandomPitch")]
    public bool RandomPitch;
    [Range(-3f,3f)]
    public float MaxPitch = 1.2f;
    [Range(-3f, 3f)]
    public float MinPitch = 0.8f;

    [Header("RandomVolume")]
    public bool RandomVolume;
    [Range(0f, 1f)]
    public float MaxVol = 1f;
    [Range(0f, 1f)]
    public float MinVol;

    private void Awake()
    {
        module_audsource = GetComponent<AudioSource>();
    }

    public virtual void Play(string base_soundKey)
    {
        module_audsource = GetComponent<AudioSource>();

        if (RandomPitch)
        module_audsource.pitch = Random.Range(MinPitch, MaxPitch);

        if(RandomVolume)
        module_audsource.volume = Random.Range(MinVol, MaxVol);
    }

    public virtual void Stop()
    {
        module_audsource.Stop();
    }
}
