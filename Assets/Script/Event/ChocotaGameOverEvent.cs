using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocotaGameOverEvent : MonoBehaviour, IEventObject
{
    [Header("ゲームオーバーシーン名")]
    [SerializeField]
    string _gameOverSceneName = "GameOver";

    [SerializeField]
    private EventData _event;

    [Header("GameOverシーンに行くまでの時間")]
    [SerializeField]
    private float _changeGameOverSceneTime = 5;

    private bool _isEventStart = false;

    private EventManager _eventManager;
    
    private FadeInOut _fadeInOut;

    public EventData EventData => _event;

    public EventTalkData ResultEventTalkData { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        _eventManager = FindObjectOfType<EventManager>();
        _event.talkEnded += TalkEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEventStart && !_fadeInOut.IsFade)
        {
            StartCoroutine(GameOverStart());
            FindObjectOfType<EventManager>().EventCheck(this);
            _isEventStart = false;
        }

        _changeGameOverSceneTime -= Time.deltaTime;
        if(_changeGameOverSceneTime < 0)
        {
            FindObjectOfType<SceneChange>().ChangeScene(_gameOverSceneName);
        }
    }

    IEnumerator GameOverStart()
    {
        GameManager.Instance.StateChange(GameManager.SystemState.GameOver);
        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.MonsterEating);
        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.MonsterEating);
        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.MonsterEating);
        yield return new WaitForSeconds(0.8f);
    }

    public void ResultFlagCheck()
    {
        ResultEventTalkData = _event.FalseTalkData;
    }

    public void TalkEnd(EventData eventData)
    {
        FindObjectOfType<SceneChange>().ChangeScene(_gameOverSceneName);
    }

    public void EventStart()
    {
        _fadeInOut = FindObjectOfType<FadeInOut>();
        _fadeInOut.FadeIn();
        _isEventStart = true;
    }
}
