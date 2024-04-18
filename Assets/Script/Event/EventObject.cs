﻿using UnityEngine.UI;
using UnityEngine;

public class EventObject : MonoBehaviour, IEventObject
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] public EventData _eventData { get; }
    public Image _resultImage { get; set; }
    public string _resultName { get; set; }
    public string _resultText { get; set; }

    public void ResultFlagCheck()
    {
        if (_eventData != null)
        {
            //調べたフラグがtrueのとき
            if (_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                _resultImage = _eventData.TrueImage;
                _resultName = _eventData.TrueName;
                _resultText = _eventData.TrueText;
            }
            else //調べたフラグがfalseのとき
            {
                _resultImage = _eventData.FalseImage;
                _resultName = _eventData.FalseName;
                _resultText = _eventData.FalseText;
            }

            //変更するフラグが設定されていれば変更する
            if (!_eventData.ChangeFlag) _flagList.SetFlag(_eventData.ChangeFlag);
        }
    }

}
