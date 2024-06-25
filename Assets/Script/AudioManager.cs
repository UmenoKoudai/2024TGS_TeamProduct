using System;
using UnityEngine;
using static AudioManager.BGM;

/// <summary>
/// ���y���Ǘ�����N���X
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("���̃V�[���ŗ���BGM��I��")]
    private BGMClip _bgmEnum;

    [SerializeField, Tooltip("BGM�̐ݒ������")]
    private BGM _bgmClass;
    public BGM BGMClass => _bgmClass;

    [SerializeField, Tooltip("SE�̐ݒ������")]
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
    /// BGM���Ǘ�����N���X
    /// </summary>
    [Serializable]
    public class BGM
    {
        [SerializeField, Tooltip("BGM��炷AudioSource")]
        private AudioSource _bgmSound;
        [SerializeField, Tooltip("�炵����BGM")]
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
    /// SE���Ǘ�����N���X
    /// </summary>
    [Serializable]
    public class SE
    {
        [SerializeField, Tooltip("SE��炷AudioSource")]
        private AudioSource _seSound;
        [SerializeField, Tooltip("�炵����SE")]
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

