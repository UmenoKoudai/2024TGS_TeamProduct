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
        RaycastHit2D[] hit = Physics2D.RaycastAll(_character.transform.position, _character.Direction, 2);
        Debug.DrawRay(_character.transform.position, _character.Direction * 5, Color.red);
        foreach(var h in hit)
        {
            if(h.collider)
            {
                Debug.Log(h.collider.gameObject.name);
            }
            if (h.collider.gameObject.TryGetComponent(out IAction action))
            {
                action.Execute(_character);
            }
            if (h.collider.gameObject.TryGetComponent(out IEventObject events))
            {
                _character.Event.EventCheck(events);
                Debug.Log("イベント呼び出し");
            }
        }
        //if(hit.collider)
        //{
        //    Debug.Log(hit.collider.gameObject.name);
        //    if (hit.collider.gameObject.TryGetComponent(out EventObject events))
        //    {
        //        _character.EventManager.EventCheck(events);
        //        Debug.Log("イベント呼び出し");
        //    }
        //}
        //if(hit.collider.gameObject.TryGetComponent(out IAction action))
        //{
        //    action.Execute();
        //}
        //if (hit.collider.gameObject.TryGetComponent(out IEventObject events))
        //{
        //    _character.EventManager.EventCheck(events);
        //    Debug.Log("イベント呼び出し");
        //}
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
