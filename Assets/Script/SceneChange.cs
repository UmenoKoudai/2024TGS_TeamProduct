using System;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SceneChange : MonoBehaviour, IEventObject
{
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
    
    private EventManager _eventManager;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    private void Awake()
    {
        _eventManager = FindObjectOfType<EventManager>();
    }

    //public void Execute(CharacterBase chara)
    //{
    //    if (_isKeyRoom != true || _event.CheckFlag.IsOn)
    //    {
    //        Debug.Log($"DIrectionCopy{chara.Direction}");
    //        PlayingData.Instance.PosName = _posName;
    //        PlayingData.Instance.Direction = chara.Direction;
    //        //GameManager.instance.StateChange(GameManager.SystemState.SceneMove);
    //        SceneManager.LoadScene(_nextScene);
    //    }
    //    else
    //    {
    //        _eventManager.EventCheck(this);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            if (_isKeyRoom != true || _event.CheckFlag.IsOn)
            {
                PlayingData.Instance.PosName = _posName;
                PlayingData.Instance.Direction = player.Direction;
                //GameManager.instance.StateChange(GameManager.SystemState.SceneMove);
                SceneManager.LoadScene(_nextScene);
            }
            else
            {
                _eventManager.EventCheck(this);
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
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
