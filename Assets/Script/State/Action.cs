using UnityEngine;

public class Action : IStateMachine
{
    private CharacterBase _character;

    public Action(CharacterBase character)
    {
        _character = character;
    }

    public void Enter()
    {
        RaycastHit2D hit = Physics2D.Raycast(_character.transform.position, _character.transform.forward);
        if(hit.collider.gameObject.TryGetComponent(out IAction action))
        {
            action.Execute();
        }
        _character.StateChange(CharacterBase.State.NormalMove);
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}
