using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FadeSystem : MonoBehaviour
{
    private Animator _anim;

    public enum AnimType
    {
        FadeIn,
        FadeOut,
        Special,
    }
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Play(AnimType type)
    {
        if(type == AnimType.FadeIn)
        {
            _anim.Play("FadeIn");
        }
        else if (type == AnimType.FadeOut)
        {
            _anim.Play("FadeOut");
        }
        else if(type == AnimType.Special)
        {
            _anim.Play("SpecialFade");
        }
    }
}
