using System;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SceneChange : MonoBehaviour, IEventObject
{
    [SerializeField, Tooltip("�J�ڂ���V�[���̖��O")]
    private string _nextScene;
    [SerializeField]
    private string _posName;

    [SerializeField]
    private bool _isKeyRoom;
    [Header("�J�M���K�v�Ȃ����ɂ͕s�v")]
    [SerializeField, Tooltip("�J�M����肵�Ă��Ȃ��Ƃ��̃C�x���g")]
    private EventData _event;
    [SerializeField, Tooltip("�t���O�̃��X�g")]
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
            //���ׂ��t���O��true�̂Ƃ�
            if (_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);

            }
            else //���ׂ��t���O��false�̂Ƃ�
            {
                ResultEventTalkData = _event.FalseTalkData;
            }

        }
    }
}
