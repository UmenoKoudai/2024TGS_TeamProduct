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
    private Vector3 _nextTile;

    public NormalMove(CharacterBase character)
    {
        _character = character;
        _start = _character.Map.WorldToCell(_character.transform.position);
        _character.transform.position = _character.Map.GetCellCenterLocal(_start);
        _nextTile = _character.transform.position;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
        _isMove = false;
        _character.Rb.velocity = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if (!_isMove) return;
        _character.Direction = _direction;
        float distance = Vector3.Distance(_nextTile, _character.transform.position);
        _character.Rb.velocity = _direction * _character.Speed;
        if (distance < 0.1f)
        {
            _isMove = false;
            _character.Rb.velocity = Vector3.zero;
        }
    }

    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Debug.DrawRay(_character.transform.position, _character.Direction * 5, Color.red);
        if (h > 0 || h < 0 || v > 0 || v < 0)
        {
            _character.Direction = _direction;
            _character.Animator.SetFloat("X", h);
            _character.Animator.SetFloat("Y", v);
        }
        if (Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && !_isMove)
        {
            if (h > 0)
            {
                var next = new Vector3(_nextTile.x + 1, _nextTile.y, _nextTile.z);
                _nextPos = _character.Map.WorldToCell(next);
                if (_character.Map.HasTile(_nextPos))
                {
                    _character.CreatePos(_nextTile);
                    _nextTile = next;
                    _direction = _nextTile - _character.transform.position;
                    _isMove = true;
                }
            }

            if (h < 0)
            {
                var next = new Vector3(_nextTile.x - 1, _nextTile.y, _nextTile.z);
                _nextPos = _character.Map.WorldToCell(next);
                if (_character.Map.HasTile(_nextPos))
                {
                    _character.CreatePos(next);
                    _nextTile = next;
                    _direction = _nextTile - _character.transform.position;
                    _isMove = true;
                }
            }
        }
        if (Input.GetButton("Vertical") && !Input.GetButton("Horizontal") && !_isMove)
        {
            if (v > 0)
            {
                var next = new Vector3(_nextTile.x, _nextTile.y + 1, _nextTile.z);
                _nextPos = _character.Map.WorldToCell(next);
                if (_character.Map.HasTile(_nextPos))
                {
                    _character.CreatePos(next);
                    _nextTile = next;
                    _direction = _nextTile - _character.transform.position;
                    _isMove = true;
                }
            }
            if (v < 0)
            {
                var next = new Vector3(_nextTile.x, _nextTile.y - 1, _nextTile.z);
                _nextPos = _character.Map.WorldToCell(next);
                if (_character.Map.HasTile(_nextPos))
                {
                    _character.CreatePos(next);
                    _nextTile = next;
                    _direction = _nextTile - _character.transform.position;
                    _isMove = true;
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Action();
        }
    }

    private void Action()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(_character.transform.position, _character.Direction, 2);
        foreach (var h in hit)
        {
            if (h.collider.gameObject.TryGetComponent(out IEventObject events))
            {
                _character.Event.EventCheck(events);
            }
        }
    }
}
