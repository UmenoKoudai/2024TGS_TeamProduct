using UnityEngine;

public class GhostEvent : MonoBehaviour, IEventObject, IAction
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private FlagData _foodFlag;
    [SerializeField]
    private Animator _animator;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    public void Execute(CharacterBase chara)
    {
    }

    public void ResultFlagCheck()
    {
        if(_event != null)
        {
            if(_flagList.GetFlagStatus(_foodFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                if (_event.CheckFlag) _flagList.SetFlag(_event.CheckFlag);
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
            }
        }
    }
}
