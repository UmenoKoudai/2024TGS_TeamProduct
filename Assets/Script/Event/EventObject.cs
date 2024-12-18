﻿using UnityEngine;

public class EventObject : MonoBehaviour, IEventObject
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] ItemManager _item;
    public EventData EventData { get => _eventData; }
    public EventTalkData ResultEventTalkData { get; set; }

    //↓追加分　ウメノ
    [SerializeField] private ParticleSystem _effect;
    //
    //public Sprite _resultImage { get; set; }
    //public string _resultName { get; set; }
    //public string _resultText { get; set; }

    public void ResultFlagCheck()
    {
        if (_eventData != null)
        {
            //調べたフラグがtrueのとき
            if (_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                ResultEventTalkData = _eventData.TrueTalkData;
            }
            else //調べたフラグがfalseのとき
            {
                ResultEventTalkData = _eventData.FalseTalkData;
                if (_item)
                {
                    _item.PickUp();
                    _effect.Stop();
                }
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }
        }
    }

}
