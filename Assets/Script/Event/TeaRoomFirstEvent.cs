using UnityEngine;
using UnityEngine.Playables;

public class TeaRoomFirstEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private PlayableDirector _timeLine;
    [SerializeField]
    private GameObject[] _firstObject;
    [SerializeField]
    private GameObject[] _secondObject;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set;}

    public void TalkStart()
    {
        FindObjectOfType<EventManager>().EventCheck(this);
    }

    public void ResultFlagCheck()
    {
        if(_event != null)
        {
            if(_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                foreach(var obj in _firstObject)
                {
                    obj.SetActive(true);
                }
                foreach(var obj in _secondObject)
                {
                    obj.SetActive(false);
                }
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
                _timeLine.Play();
                if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);
            }
        }
    }
}
