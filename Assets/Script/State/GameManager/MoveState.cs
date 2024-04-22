using UnityEngine;

public class MoveState : IStateMachine
{
    private GameManager _gameManager;
    private PlayCharacter _playChara = PlayCharacter.Player;
    enum PlayCharacter
    {
        Player,
        Girl,
    }

    public MoveState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        if (_playChara == PlayCharacter.Player)
        {
            _gameManager.Player.ManualUpdate();
            _gameManager.Player.ManualFixedUpdate();
        }
        else
        {
            _gameManager.Girl.ManualUpdate();
            _gameManager.Girl.ManualFixedUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_playChara == PlayCharacter.Player)
            {
                _playChara = PlayCharacter.Girl;
            }
            else
            {
                _playChara = PlayCharacter.Player;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) _gameManager.StateChange(GameManager.SystemState.Option);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Option");
            _gameManager.StateChange(GameManager.SystemState.Option);
        }
    }
}
