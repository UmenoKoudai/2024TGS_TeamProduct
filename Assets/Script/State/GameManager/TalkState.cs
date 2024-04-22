using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkState : IStateMachine
{
    private GameManager _gameManager;
    public TalkState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        _gameManager.TalkPanel.SetActive(true);
    }

    public void Exit()
    {
        _gameManager.TalkPanel.SetActive(false);
        _gameManager.StateChange(GameManager.SystemState.Move);
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1")) Exit();
    }
}
