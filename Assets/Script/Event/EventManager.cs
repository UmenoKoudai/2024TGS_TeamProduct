using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] TalkSystem _talkSystem;

    /// <summary> Playerが対象物を調べるとき、Playerから呼ぶ </summary>
    /// <param name="eventObject"></param>
    public void EventCheck(IEventObject eventObject)
    {
        eventObject.ResultFlagCheck();
        _talkSystem.ShowMessage(eventObject.ResultName, eventObject.ResultText, eventObject.ResultImage);
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
