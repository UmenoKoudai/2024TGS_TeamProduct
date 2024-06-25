using UnityEngine;

public class EventActionArea : MonoBehaviour, IEventObject
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] EventManager _eventManager = null;
    [SerializeField, SerializeReference]
   // [SubclassSelector]
    IAction _action;

    private CharacterBase _character;

    public EventData EventData => _eventData;

    public EventTalkData ResultEventTalkData { get; set; }

    public void ResultFlagCheck()
    {
        if (_eventData != null)
        {
            //���ׂ��t���O��true�̂Ƃ�
            if (_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                ResultEventTalkData = _eventData.TrueTalkData;
                //�ύX����t���O���ݒ肳��Ă���ΕύX����
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);

            }
            else //���ׂ��t���O��false�̂Ƃ�
            {
                ResultEventTalkData = _eventData.FalseTalkData;
                _action.Execute(_character);
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CharacterBase>(out var character))
        {
            _eventManager.EventCheck(this);
            _character = character;
        }
    }

}
