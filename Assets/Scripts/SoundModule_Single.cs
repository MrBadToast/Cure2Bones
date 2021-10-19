using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SoundModule_Single : SoundModule_Base
{
    [SerializeField, Sirenix.OdinInspector.Title("SoundClips")]
    Dictionary<string,AudioClip> sound;

    public override void Play(string soundKey)
    {
        if(!sound.ContainsKey(soundKey))
            Debug.LogError(soundKey + " 사운드 키를 Sound Module에서 찾을 수 없습니다.");

        base.Play(string.Empty);

        base.module_audsource.clip = sound[soundKey];
        base.module_audsource.Play();
    }
}
