using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour, IEventObject
{
    [SerializeField, Tooltip("どのシーンで使用するか")]
    private SceneType _sceneType = SceneType.InGame;
    [SerializeField]
    private bool _isFade = true;
    [SerializeField, Tooltip("遷移するシーンの名前")]
    private string _nextScene;
    [SerializeField]
    private string _posName;
    [SerializeField]
    private bool _isKeyRoom;

    [Header("カギが必要ない扉には不要")]
    [SerializeField, Tooltip("カギを入手していないときのイベント")]
    private EventData _event;
    [SerializeField, Tooltip("フラグのリスト")]
    private FlagList _flagList;
    [SerializeField]
    private string _useItemName;

    private FadeSystem _fadeSystem;
    private EventManager _eventManager;
    private ItemInventry _itemInventry;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    enum SceneType
    {
        Title,
        InGame,
        GameEnd,
    }

    private void Awake()
    {
        if (_sceneType == SceneType.InGame)
        {
            _eventManager = FindObjectOfType<EventManager>();
        }
    }

    private void Start()
    {
        _fadeSystem = FindObjectOfType<FadeSystem>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            if (_isKeyRoom != true || _event.CheckFlag.IsOn)
            {
                _itemInventry = FindObjectOfType<ItemInventry>();
                _itemInventry.ItemUse(_useItemName);
                PlayingData.Instance.PosName = _posName;
                PlayingData.Instance.Direction = player.Direction;
                SceneManager.LoadScene(_nextScene);
            }
            else
            {
                _eventManager.EventCheck(this);
            }
        }
    }
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            if (_isKeyRoom != true || _event.CheckFlag.IsOn)
            {
                _itemInventry = FindObjectOfType<ItemInventry>();
                _itemInventry.ItemUse(_useItemName);
                PlayingData.Instance.PosName = _posName;
                PlayingData.Instance.Direction = player.Direction;
                AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.NewDoorOpen);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                SceneManager.LoadScene(_nextScene);
            }
            else
            {
                _eventManager.EventCheck(this);
            }
        }
    }

    public async void ChangeScene(string sceneName)
    {
        //_fadeSystem.Play(FadeSystem.AnimType.FadeOut);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        SceneManager.LoadScene(sceneName);
    }

    public void ResultFlagCheck()
    {
        if (_event != null)
        {
            //調べたフラグがtrueのとき
            if (_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);

            }
            else //調べたフラグがfalseのとき
            {
                ResultEventTalkData = _event.FalseTalkData;
            }

        }
    }
}
