using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>行動するかどうかの選択中の状態</summary>
public class SelectState : IStateMachine
{
    private GameManager _gameManager;

    private EventManager _eventManager;

    private EventSystem _eventSystem;

    private GameObject _yesButton;

    private GameObject _noButton;
    public SelectState(GameManager gameManager)
    {
        _gameManager = gameManager;
        _eventManager = gameManager.EventManager;
        _eventSystem = gameManager.EventSystem;
    }

    public void Enter()
    {
        _gameManager.PanelManager.SelectPanel.SetActive(true);
        _yesButton = _gameManager.PanelManager.SelectPanel.transform.GetChild(0).gameObject;
        _noButton = _gameManager.PanelManager.SelectPanel.transform.GetChild(1).gameObject;
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
        float y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Vertical") && y > 0)
        {
            EventSystem.current.SetSelectedGameObject(_yesButton);
        }
        else if(Input.GetButtonDown("Vertical") && y < 0)
        {
            EventSystem.current.SetSelectedGameObject(_noButton);
        }

        if (!_eventManager.SelectSystem.IsSelecting)
        {
            AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.ButtonClick);
            _eventManager.TalkSystem.IsEventSelectTalk(_eventManager.SelectSystem.IsYes);
            Exit();
        }
    }
}
