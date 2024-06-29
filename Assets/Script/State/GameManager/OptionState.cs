using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionState : IStateMachine
{
    private GameManager _gameManager;
    public OptionState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public void Enter()
    {
        _gameManager.PanelManager.OptionPanel?.SetActive(true);
    }

    public void Exit()
    {
        _gameManager.PanelManager.OptionPanel?.SetActive(false);
        _gameManager.StateChange(GameManager.SystemState.Move);
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { Exit(); }
    }
}
