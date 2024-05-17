using UnityEngine;

public class NormalMove : IStateMachine
{
    private CharacterBase _character;
    private bool _isUpMove = true;
    private bool _isLeftMove = false;
    private bool _isRightMove = false;
    private Vector3Int _start;
    private bool _isMove = false;
    private Vector3 _direction;
    private Vector3Int _nextPos;

    public NormalMove(CharacterBase character)
    {
        _character = character;
        _start = _character.Map.WorldToCell(_character.transform.position);
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
        if (!_isMove) return;
        float distance = Vector3.Distance(_nextPos, _character.transform.position);
        _character.Rb.velocity = _direction * _character.Speed;
        if (distance < 0.5f) 
        {
            _isMove = false;
            _start = _nextPos;
            _character.Rb.velocity = Vector3.zero;
        }
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
                _nextPos = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_nextPos))
                {
                    _direction = _nextPos - _start;
                    _isMove = true;
                }
            }

            if (h < 0)
            {
                _isUpMove = false;
                _isRightMove = false;
                _isLeftMove = true;
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x - 1, _start.y, _start.y));
                _nextPos = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _direction = _nextPos - _start;
                    _isMove = true;
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
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x, _start.y + 1, _start.y));
                _nextPos = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _direction = _nextPos - _start;
                    _isMove = true;
                }
            }
            if (v < 0)
            {
                _isUpMove = false;
                _isRightMove = false;
                _isLeftMove = false;
                var nextTile = _character.Map.GetCellCenterWorld(new Vector3Int(_start.x, _start.y - 1, _start.y));
                _nextPos = _character.Map.WorldToCell(nextTile);
                if (_character.Map.HasTile(_start))
                {
                    _direction = _nextPos - _start;
                    _isMove = true;
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
