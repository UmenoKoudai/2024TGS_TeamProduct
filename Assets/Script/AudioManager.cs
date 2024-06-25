using System;
using UnityEngine;
using static AudioManager.BGM;

/// <summary>
/// 音楽を管理するクラス
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("このシーンで流すBGMを選択")]
    private BGMClip _bgmEnum;

    [SerializeField, Tooltip("BGMの設定をする")]
    private BGM _bgmClass;
    public BGM BGMClass => _bgmClass;

    [SerializeField, Tooltip("SEの設定をする")]
    private SE _seClass;
    public SE SeClass => _seClass;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    private void Start()
    {
        AudioManager.Instance._bgmClass.Play(_bgmEnum);
    }

    /// <summary>
    /// BGMを管理するクラス
    /// </summary>
    [Serializable]
    public class BGM
    {
        [SerializeField, Tooltip("BGMを鳴らすAudioSource")]
        private AudioSource _bgmSound;
        [SerializeField, Tooltip("鳴らしたいBGM")]
        private AudioClip[] _bgmClip;

        public enum BGMClip
        {
            Title,
            Game,
            Result,
        }

        public void Play(BGMClip clip)
        {
            _bgmSound.PlayOneShot(_bgmClip[(int)clip]);
        }

    }

    /// <summary>
    /// SEを管理するクラス
    /// </summary>
    [Serializable]
    public class SE
    {
        [SerializeField, Tooltip("SEを鳴らすAudioSource")]
        private AudioSource _seSound;
        [SerializeField, Tooltip("鳴らしたいSE")]
        private AudioClip[] _seClip;

        public enum SEClip
        {
            Attack,
            BuffAbility,
            Recovery,
            Click,
            Kick,
            KO,
            Bomb,
        }

        public void Play(SEClip clip)
        {
            _seSound.PlayOneShot(_seClip[(int)clip]);
        }
    }
}

