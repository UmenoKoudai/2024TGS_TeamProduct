using UnityEngine.UI;
using UnityEngine;

public class EventObject : MonoBehaviour, IEventObject, IAction
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] ItemManager _item;
    public EventData EventData { get => _eventData; }
    public Sprite ResultImage { get; set; }
    public string ResultName { get; set; }
    public string ResultText { get; set; }

    //↓追加分　ウメノ
    [SerializeField] private ParticleSystem _effect;
    //
    public Sprite _resultImage { get; set; }
    public string _resultName { get; set; }
    public string _resultText { get; set; }
    //↓追加分　ウメノ
    public void Execute(CharacterBase chara)
    {
        if (_eventData.CheckFlag.IsOn) return;
        _item.PickUp();
        _effect.Stop();
    }
    //
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
                //if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);

            }
            else //調べたフラグがfalseのとき
            {
                ResultImage = _eventData.FalseImage;
                ResultName = _eventData.FalseName;
                ResultText = _eventData.FalseText;
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }

        }
    }

}
