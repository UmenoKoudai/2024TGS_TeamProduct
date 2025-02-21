using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStartEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    bool _isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (_isFirst && !_flagList.GetFlagStatus(_event.CheckFlag))
        {
            FindObjectOfType<EventManager>().EventCheck(this);
            _isFirst = false;
        }
    }

    public void ResultFlagCheck()
    {
        if (_event != null)
        {
              ResultEventTalkData = _event.FalseTalkData;
                
              if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);
        }
    }
}
