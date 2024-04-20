using UnityEngine;

public class Player : CharacterBase
{
    private State _state;
    public State State
    {
        get => _state;
        set
        {
            if (_state == value) return;
            _state = value;
            switch (_state)
            {
                case State.NormalMove:
                    _normalMove.Enter();
                    break;
                case State.Action:
                    _action.Enter();
                    break;
            }
        }
    }

    private NormalMove _normalMove;
    private Action _action;

    public void Init()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _normalMove = new NormalMove(this);
        _action = new Action(this);
    }

    public void ManualUpdate()
    {
        switch (_state)
        {
            case State.NormalMove:
                _normalMove.Update(); 
                break;
            case State.Action:
                _action.Update();
                break;
        }
    }

    public void ManualFixedUpdate()
    {
        switch (_state)
        {
            case State.NormalMove:
                _normalMove.FixedUpdate();
                break;
            case State.Action:
                _action.FixedUpdate();
                break;
        }
    }

    public override void StateChange(State changeState)
    {
        State = changeState;
    }
}