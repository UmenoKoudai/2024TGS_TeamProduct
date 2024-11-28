using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;

public class UseItemEvent : MonoBehaviour, IEventObject
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
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
            }
            else //���ׂ��t���O��false�̂Ƃ�
            {
                ResultEventTalkData = _eventData.FalseTalkData;
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }
        }
    }
}
