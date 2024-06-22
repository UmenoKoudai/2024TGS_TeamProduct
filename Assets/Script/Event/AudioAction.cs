using System;
using UnityEngine;

[Serializable]
public class AudioAction : IAction
{
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _clip;

    public void Execute(CharacterBase chara)
    {
        _audio.PlayOneShot(_clip);
    }
}
