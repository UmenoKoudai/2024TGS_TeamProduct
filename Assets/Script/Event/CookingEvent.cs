using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private ItemManager _itemManager;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    public void ResultFlagCheck()
    {
        if(_event != null)
        {
            if(_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                if (_itemManager != null) _itemManager.PickUp();
                if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
            }
        }
    }
}
