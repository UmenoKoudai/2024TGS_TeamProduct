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
            /// <summary>タイトル</summary>
            Title,
            /// <summary>通常の屋敷内</summary>
            InGameNormal,
            /// <summary>敵に追われている時</summary>
            InGameBeChased,
            /// <summary>ゲームオーバー</summary>
            GameOver,
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
            /// <summary>Playerの足音</summary>
            Footsteps,
            /// <summary>古い扉が開く音</summary>
            OldDoorOpenOne,
            /// <summary>古い扉が開く音</summary>
            OldDoorOpenTwe,
            /// <summary>新しめな扉が開く音</summary>
            NewDoorOpen,
            /// <summary>料理音</summary>
            Cooking,
            /// <summary>モンスターうめき声</summary>
            MonsterMoan,
            /// <summary>モンスターに食べられる音</summary>
            MonsterEating,
            /// <summary>クリック音</summary>
            Click,
            /// <summary>ボタンクリック音</summary>
            ButtonClick,
            /// <summary>アイテム取得音</summary>
            ItemGet,
            /// <summary>次のテキストに行く音</summary>
            NextTalkTextDisplay,
        }

        public void Play(SEClip clip)
        {
            _seSound.PlayOneShot(_seClip[(int)clip]);
        }
    }
}

