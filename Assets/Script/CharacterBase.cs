using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    [SerializeField, Tooltip("キャラのスピード")]
    private float _speed;
    public float Speed => _speed;
    [SerializeField, Tooltip("キャラの移動可能なタイルマップ")]
    private Tilemap _groundMap;

    public Tilemap Map => _groundMap;

    public EventManager Event { get; set; }

    public Rigidbody2D Rb { get; set; }
    public Vector2 Direction { get; set; }
    public Animator Animator { get; set; }
    public enum State
    {
        NormalMove,
        MacCount,
    }

    private State _state;
    public State MoveState
    {
        get => _state;
        set
        {
            if (_state == value) return;
            _state = value;
            _currentState = _states[(int)_state];
            _currentState.Enter();
        }
    }

    private IStateMachine[] _states = new IStateMachine[(int)State.MacCount];
    private IStateMachine _currentState;

    public void Init(EventManager eventManager)
    {
        if (!Animator) Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        _states[(int)State.NormalMove] = new NormalMove(this);
        Event = eventManager;
        SetDirection(Direction);
        _currentState = _states[(int)_state];
    }

    public void ManualUpdate()
    {
        _currentState.Update();
        //try
        //{
        //    _currentState.Update();
        //}
        //catch
        //{
        //    Debug.LogError($"ステートが設定されていません{this.gameObject.GetType()}:{name}");
        //}
    }

    public void ManualFixedUpdate()
    {
        try
        {
            _currentState.FixedUpdate();
        }
        catch
        {
            Debug.LogError($"ステートが設定されていません{this.gameObject.GetType()}");
        }
    }

    public virtual void CreatePos(Vector3 pos)
    {
        Debug.LogError("CreatePos関数をオーバーライドしていません");
    }

    public void StateChange(State changeState)
    {
        MoveState = changeState;
    }

    public void SetDirection(Vector2 direction)
    {
        Animator.SetFloat("X", direction.x);
        Animator.SetFloat("Y", direction.y);
    }
}
