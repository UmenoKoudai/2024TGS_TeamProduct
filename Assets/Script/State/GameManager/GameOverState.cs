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
        _gameManager.Player.Rb.velocity = Vector2.zero;
        _gameManager.Girl.Rb.velocity = Vector2.zero;
    }

    public void Exit() { }

    void IStateMachine.Update() { }

    public void FixedUpdate() { }
}
