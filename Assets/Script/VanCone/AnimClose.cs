using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimClose : MonoBehaviour
{
    [SerializeField]
    private GameObject _animObject;

    public void AnimEnd()
    {
        _animObject.SetActive(false);
    }
}
