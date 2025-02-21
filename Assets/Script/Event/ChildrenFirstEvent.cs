using UnityEngine;

public class ChildrenFirstEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private GameObject _firstObject;
    [SerializeField]
    private GameObject _secondObject;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private EventManager _eventManager;

    [SerializeField]
    private Sprite _girlLeft;
    [SerializeField]
    private Sprite _girlRight;
    [SerializeField]
    private Sprite _girlForward;

    FollowSystem _followSystem;

    bool _isGirl = false; 

    bool _isTalk = false;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    private void Start()
    {
        if (_flagList.GetFlagStatus(_event.CheckFlag))
        {
            _firstObject.SetActive(false);
            _secondObject.SetActive(true);
            Destroy(this);
        }
        _isGirl = false;
    }

    private void Update()
    {
        if(!_isGirl)
        {
            if (_flagList.GetFlagStatus(_event.CheckFlag))
            {
                if (!_eventManager.TalkSystem.IsTalking)
                {
                    _firstObject.SetActive(false);
                    _secondObject.SetActive(true);
                    _followSystem = _secondObject.GetComponent<FollowSystem>();
                    if (_followSystem.Target.transform.position.x < _secondObject.transform.position.x)
                    {
                        _followSystem.ForwardX = -1;
                    }
                    else
                    {
                        _followSystem.ForwardX = 1;
                    }
                    _secondObject.GetComponent<FollowSystem>().FollowStart();
                    _isGirl = true;
                }
            }
            else
            {
                if (!_eventManager.TalkSystem.IsTalking)
                {
                    SpriteRenderer spriteRenderer = _firstObject.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = _girlForward;
                }
            }
        }
    }

    public void ResultFlagCheck()
    {
        if(_event != null)
        {
            if(!_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.FalseTalkData;
                _secondObject.transform.position = _firstObject.transform.position;
                SpriteRenderer spriteRenderer = _firstObject.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
                if (_player.transform.position.x <= _firstObject.transform.position.x)
                {
                    spriteRenderer.sprite = _girlLeft;
                }
                else
                {
                    spriteRenderer.sprite = _girlRight;
                }
                //if (_event.ChangeFlag != null) _flagList.SetFlag(_event.ChangeFlag);
            }
        }
    }

    public void SetFlag()
    {
        _flagList.SetFlag(_event.ChangeFlag);
    }
}
