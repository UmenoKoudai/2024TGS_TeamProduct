using UnityEngine;
using UnityEngine.EventSystems;

public class TalkState : IStateMachine
{
    private GameManager _gameManager;

    private EventManager _eventManager;

    private EventSystem _eventSystem;
    public TalkState(GameManager gameManager)
    {
        _gameManager = gameManager;
        _eventManager = gameManager.EventManager;
        _eventSystem = gameManager.EventSystem;
    }

    public void Enter()
    {
        _gameManager.PanelManager.TalkPanel.SetActive(true);
    }

    public void Exit()
    {
        _gameManager.PanelManager.TalkPanel.SetActive(false);
        _gameManager.StateChange(GameManager.SystemState.Move);
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        //クドウ追記
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_eventManager.TalkSystem.IsTalkTextDisplaying)  //会話Text表示中なら
            {
                _eventManager.TalkSystem.TalkTextAllDisplay();     //全て表示
            }
            else
            {
                _eventManager.TalkSystem.OnUpdateMessage();　　//Talkの更新
            }
        }

        if (!_eventManager.TalkSystem.IsTalking)
        {
            if (_eventManager.TalkSystem.IsSelectEventTalk)
            {
                _gameManager.StateChange(GameManager.SystemState.Select);
            }
            else
            {
                Exit();
            }
        }
    }
}
