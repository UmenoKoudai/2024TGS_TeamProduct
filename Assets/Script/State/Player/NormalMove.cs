using UnityEngine;

public class NormalMove : IStateMachine
{
    private Vector2 _direction;
    private CharacterBase _character;
    private bool _isUpMove = true;
    private bool _isLeftMove = false;
    private bool _isRightMove = false;
    private Vector3Int _start;

    public NormalMove(CharacterBase character)
    {
        _character = character;
        _start = _character.Map.WorldToCell(_character.transform.position);
        Debug.Log(_start);
        _character.transform.position = _start;
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
        _character.Animator.SetBool("Up", _isUpMove);
        _character.Animator.SetBool("Left", _isLeftMove);
        _character.Animator.SetBool("Right", _isRightMove);
        if(h > 0 || h < 0 || v > 0 || v < 0) _character.Direction = _direction;
        if (Input.GetButtonDown("Horizontal") && !Input.GetButtonDown("Vertical"))
        {
            if (h > 0)
            {
                _isUpMove = false;
                _isRightMove = true;
                _isLeftMove = false;
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x + 1, _start.y, _start.y));
                _start = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _character.transform.Translate(_start);
                }
            }

            if (h < 0)
            {
                _isUpMove = false;
                _isRightMove = false;
                _isLeftMove = true;
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x - 1, _start.y, _start.y));
                _start = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _character.transform.Translate(_start);
                }
            }
        }
        if(Input.GetButtonDown("Vertical") && !Input.GetButtonDown("Horizontal"))
        {
            if (v > 0)
            {
                _isUpMove = true;
                _isRightMove = false;
                _isLeftMove = false;
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x, _start.y - 1, _start.y));
                _start = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _character.transform.Translate(_start);
                }
            }
            if (v < 0)
            {
                _isUpMove = false;
                _isRightMove = false;
                _isLeftMove = false;
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x, _start.y - 1, _start.y));
                _start = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _character.transform.Translate(_start);
                }
            }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            Exit();
            _character.StateChange(CharacterBase.State.Action);
        }
    }
}
