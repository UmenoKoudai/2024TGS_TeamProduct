using UnityEngine;

public class GameOverState : IStateMachine
{
    private GameManager _gameManager;

    public GameOverState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        _gameManager.Player.Speed = 0;
        _gameManager.Girl.Speed = 0;
    }

    public void Exit() { }

    void IStateMachine.Update() { }

    public void FixedUpdate() { }
}
