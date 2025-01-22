using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FadeSystem : MonoBehaviour
{
    [SerializeField]
    private AnimType _type;
    private Animator _anim;

    public enum AnimType
    {
        Normal,
        Special,
    }
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Play()
    {
        if(_type == AnimType.Normal)
        {
            _anim.Play("NormalFade");
        }
        else if(_type == AnimType.Special)
        {
            _anim.Play("SpecialFade");
        }
    }
}
