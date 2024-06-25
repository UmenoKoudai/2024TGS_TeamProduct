using UnityEngine;

public class EventManager  : MonoBehaviour
{
    [SerializeField] TalkSystem _talkSystem;
    [SerializeField] SelectSystem _selectSystem;

    public TalkSystem TalkSystem => _talkSystem;
    public SelectSystem SelectSystem => _selectSystem;

    /// <summary> Playerが対象物を調べるとき、Playerから呼ぶ </summary>
    /// <param name="eventObject"></param>
    public void EventCheck(IEventObject eventObject)
    {
        eventObject.ResultFlagCheck();
        //_talkSystem.ShowMessage(eventObject.ResultEventTalkData.NormalTalk.Name, eventObject.ResultEventTalkData.NormalTalk.Sentences, 
        //eventObject.ResultEventTalkData.NormalTalk.Image, eventObject.ResultEventTalkData.IsSelectTalk);
        EventTalkData data = eventObject.ResultEventTalkData;
        _talkSystem.ShowMessage(data);
        if (data.IsSelectTalk)
        {
            _selectSystem.SetButtonText(data.YesSelectText, data.NoSelectText);
        }
    }

    /// <summary>↓人と物で仕様が別なら人のとき用</summary>
    public void TalkGet()
    {
        //だれを調べたのか確認
        //どのフラグを確認するのか判定
        //フラグを確認
        //フラグによって取り出す情報を確認・反映
    }
}
