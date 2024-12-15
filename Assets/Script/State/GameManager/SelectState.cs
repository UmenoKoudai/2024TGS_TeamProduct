using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>行動するかどうかの選択中の状態</summary>
public class SelectState : IStateMachine
{
    private GameManager _gameManager;

    private EventManager _eventManager;

    private EventSystem _eventSystem;

    public SelectState(GameManager gameManager)
    {
        _gameManager = gameManager;
        _eventManager = gameManager.EventManager;
        _eventSystem = gameManager.EventSystem;
    }

    public void Enter()
    {
        _gameManager.PanelManager.SelectPanel.SetActive(true);
        _eventManager.SelectSystem.IsSelecting = true;
    }

    public void Exit()
    {
        _gameManager.PanelManager.SelectPanel.SetActive(false);
        _eventSystem.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(null);
        _gameManager.StateChange(GameManager.SystemState.Talk);
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {

        if (!_eventManager.SelectSystem.IsSelecting)
        {
            AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.ButtonClick);
            _eventManager.TalkSystem.IsEventSelectTalk(_eventManager.SelectSystem.SelectedButtonID);
            Exit();
        }
    }
}
