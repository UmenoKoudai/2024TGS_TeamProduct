using UnityEngine;

public class WindowKnock : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventData _event;
    [SerializeField]
    private FlagList _flagList;
    [SerializeField]
    private Animator _ghostAnim;
    public Animator _GhostAnim { get => _ghostAnim; }

    private Animator _windowAnim;
    private AudioSource _audio;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }

    public void ResultFlagCheck()
    {
        if (_event != null)
        {
            if (_flagList.GetFlagStatus(_event.CheckFlag))
            {
                ResultEventTalkData = _event.FalseTalkData;
            }
            else
            {
                ResultEventTalkData = _event.TrueTalkData;
                if (_event.ChangeFlag) _flagList.SetFlag(_event.ChangeFlag);
            }
        }
    }

    //public void EventStart()
    //{
    //    _audio.Stop();
    //    _windowAnim.StopPlayback();
    //    _event.CheckFlag.SetFlagStatus(false);
    //    _ghostAnim.gameObject.SetActive(true);
    //    _ghostAnim.Play("WindowGhost");
    //}

    //public void EventClose()
    //{
    //    _event.CheckFlag.SetFlagStatus(false);
    //}

    //public void TalkEnd(EventData date)
    //{
    //    _ghostAnim.gameObject.SetActive(false);
    //    Debug.Log("トーク終了");
    //}

    private void Start()
    {
        //_event.talkEnded += TalkEnd;
        _windowAnim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        _windowAnim.SetBool("On", _event.CheckFlag.IsOn);
        if (_event.CheckFlag)
        {
            _audio.Play();
        }
    }
}
