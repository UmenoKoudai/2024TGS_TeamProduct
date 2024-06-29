using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEvent : MonoBehaviour, IEventObject, IAction
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] SavePoint _save;
    public EventData EventData => _eventData;
    public EventTalkData ResultEventTalkData { get; set; }

    public void Execute(CharacterBase chara)
    {
        Debug.Log("SaveAction");
        SaveLoadManager.Instance.SaveAction();
    }

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
            SaveLoadManager.Instance.SaveAction();
        }
    }
}
