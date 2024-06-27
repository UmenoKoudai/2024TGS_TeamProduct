using UnityEngine;

public class GhostEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private Animator _animator;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    private void Update()
    {
        if(_animator)
        {
            _animator.SetBool("IsMove", _event.ChangeFlag.IsOn);
        }
    }

    public void ResultFlagCheck()
    {
        if(_event != null)
        {
            if(_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                if (_event.ChangeFlag) _flagList.SetFlag(_event.ChangeFlag);
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
            }
        }
    }
}
