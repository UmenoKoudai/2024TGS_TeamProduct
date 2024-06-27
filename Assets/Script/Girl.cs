using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Girl : CharacterBase
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

    public void Init(EventManager eventManager)
    {
        Rb = GetComponent<Rigidbody2D>();
        _normalMove = new NormalMove(this);
        _action = new Action(this);
        Event = eventManager;
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
