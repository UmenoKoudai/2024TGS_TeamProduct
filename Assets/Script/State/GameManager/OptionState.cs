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
        Debug.Log("Option");
        _gameManager.OptionPanel.SetActive(true);
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {

    }
}
