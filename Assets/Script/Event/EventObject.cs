using UnityEngine.UI;
using UnityEngine;

public class EventObject : MonoBehaviour, IEventObject
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    public EventData EventData { get => _eventData; }
    public Image ResultImage { get; set; }
    public string ResultName { get; set; }
    public string ResultText { get; set; }

    public void ResultFlagCheck()
    {
        if (_eventData != null)
        {
            //調べたフラグがtrueのとき
            if (_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                ResultImage = _eventData.TrueImage;
                ResultName = _eventData.TrueName;
                ResultText = _eventData.TrueText;
                //変更するフラグが設定されていれば変更する
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);

            }
            else //調べたフラグがfalseのとき
            {
                ResultImage = _eventData.FalseImage;
                ResultName = _eventData.FalseName;
                ResultText = _eventData.FalseText;
            }

        }
    }

}
