using UnityEngine;

public class SearchObject : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    public void ResultFlagCheck()
    {
        if(_event != null)
        {
            if(_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
            }
        }
    }
}
