using UnityEngine;

public class SaveEvent : MonoBehaviour, IEventObject
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] SavePoint _save;
    public EventData EventData => _eventData;
    public EventTalkData ResultEventTalkData { get; set; }

    public void ResultFlagCheck()
    {
        if(_eventData != null)
        {
            if(_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                ResultEventTalkData = _eventData.TrueTalkData;
            }
            else
            {
                ResultEventTalkData = _eventData.FalseTalkData;
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }
        }
    }
}
