using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [Header("フェード時間")]
    [SerializeField]
    private float _fadeTime = 1.5f;

    private Image _background;

    [Header("最初に行うかどうか")]
    [SerializeField]
    private bool _firstFade = false;

    [Header("最初に行うのフェードパターン")]
    [SerializeField]
    private Fade _fadePattern = Fade.In;

    /// <summary>フェード中</summary>
    bool _isFade = false;

    public bool IsFade => _isFade;

    public enum Fade
    {
        In,
        Out
    }

    // Start is called before the first frame update
    void Start()
    {
        _background = this.GetComponent<Image>();

        if(_firstFade )
        {
            _background.enabled = true;
            switch (_fadePattern)
            {
                case Fade.In:
                    FadeIn();
                    break;

                case Fade.Out:
                    FadeOut();
                    break;
            }
        }
        else
        {
            _background.enabled = false; 
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        _isFade = true;
        _background.enabled = true;
        Color startColor = Color.black;
        startColor.a = 0f;
        _background.color = startColor;
        _background.DOFade(endValue: 1f, duration: _fadeTime).OnComplete(() => _isFade = false); 
    }

    public void FadeOut()
    {
        _isFade = false;
        _background.enabled = true;
        Color startColor = Color.black;
        startColor.a = 1f;
        _background.color = startColor;
        _background.DOFade(endValue: 0f, duration: _fadeTime).OnComplete(() => _isFade = false);
    }
}
