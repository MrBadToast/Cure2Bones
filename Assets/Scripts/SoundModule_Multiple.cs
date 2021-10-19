using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SoundModule_Multiple : SoundModule_Base
{
    [SerializeField, Sirenix.OdinInspector.Title("SoundClips")]
    Dictionary<string, AudioClip[]> sounds;

    public override void Play(string soundKey)
    {
        if (!sounds.ContainsKey(soundKey))
        {
            Debug.LogError(soundKey + " 사운드 키를 Sound Module에서 찾을 수 없습니다.");
            return;
        }

        base.Play(string.Empty);

        AudioClip[] soundsToPlay = sounds[soundKey];

        base.module_audsource.clip = soundsToPlay[Random.Range(0, soundsToPlay.Length)];
        base.module_audsource.Play();
    }
}
