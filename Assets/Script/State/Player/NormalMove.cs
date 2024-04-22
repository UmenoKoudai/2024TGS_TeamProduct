using UnityEngine;

public class NormalMove : IStateMachine
{
    private Vector2 _direction;
    private CharacterBase _character;
    private bool _isUpMove = true;
    private bool _isLeftMove = false;
    private bool _isRightMove = false;

    public NormalMove(CharacterBase character)
    {
        _character = character;
    }

    public void Enter()
    {
    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        _character.Rb.velocity = _direction * _character.Speed;
    }

    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _direction = new Vector2(h, v).normalized;
        _character.Animator.SetBool("Up", _isUpMove);
        _character.Animator.SetBool("Left", _isLeftMove);
        _character.Animator.SetBool("Right", _isRightMove);
        if(h > 0 || h < 0 || v > 0 || v < 0) _character.Direction = _direction;
        if (h > 0)
        {
            _isUpMove = false;
            _isRightMove = true;
            _isLeftMove = false;
        }

        if(h < 0)
        {
            _isUpMove = false;
            _isRightMove = false;
            _isLeftMove = true;
        }
        if(v > 0)
        {
            _isUpMove = true;
            _isRightMove = false;
            _isLeftMove = false;
        }
        if (v < 0)
        {
            _isUpMove = false;
            _isRightMove = false;
            _isLeftMove = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Exit();
            _character.StateChange(CharacterBase.State.Action);
        }
    }
}
