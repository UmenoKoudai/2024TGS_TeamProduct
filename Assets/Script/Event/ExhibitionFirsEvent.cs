using UnityEngine;
using UnityEngine.Playables;

public class ExhibitionFirsEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private PlayableDirector _timeLine;
    [SerializeField]
    private PlayableDirector _afterTimeLine;
    [SerializeField]
    private GameObject[] _firstObject;
    [SerializeField]
    private GameObject[] _secondObject;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    private void Start()
    {
        _event.talkEnded += TalkEnd;
        if (_event.CheckFlag.IsOn)
        {
            foreach (var obj in _firstObject)
            {
                obj.SetActive(true);
            }
            foreach (var obj in _secondObject)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            _timeLine.Play();
        }
    }

    public void TalkStart()
    {
        FindObjectOfType<EventManager>().EventCheck(this);
    }

    public void TalkEnd(EventData data)
    {
        _afterTimeLine.Play();
    }

    public void ResultFlagCheck()
    {
        if (_event != null)
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
                _timeLine.Play();
                ResultEventTalkData = _event.FalseTalkData;
                if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);
            }
        }
    }
}
