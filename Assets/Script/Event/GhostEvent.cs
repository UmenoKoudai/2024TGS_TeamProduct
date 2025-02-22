using UnityEngine;

public class GhostEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private EventData _girlKeyEvent;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ItemManager _item;
    [SerializeField]
    private string _useItemName;

    private ItemInventry _itemInventry;

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
        _itemInventry = FindObjectOfType<ItemInventry>();
        if (_event != null)
        {
            if(_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.TrueTalkData;
                _item.PickUp();
                _itemInventry.ItemUse(_useItemName);
                if (_event.ChangeFlag) _flagList.SetFlag(_event.ChangeFlag);
                if (_girlKeyEvent.ChangeFlag) _flagList.SetFlag(_girlKeyEvent.ChangeFlag);
            }
            else
            {
                ResultEventTalkData = _event.FalseTalkData;
            }
        }
    }
}
