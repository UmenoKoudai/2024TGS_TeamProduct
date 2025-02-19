using UnityEngine;
using UnityEngine.Playables;

public class TeaRoomFirstEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagData _girlFlag;
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

    private void Start()
    {
        if(_event.CheckFlag.IsOn && _girlFlag.IsOn)
        {
            foreach (var obj in _firstObject)
            {
                obj.SetActive(true);
            }
            foreach (var obj in _secondObject)
            {
                obj.SetActive(false);
            }
            _timeLine.Play();
        }
        else
        {
            _firstObject[0].SetActive(true);
        }
    }

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
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
                if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);
            }
        }
    }
}
