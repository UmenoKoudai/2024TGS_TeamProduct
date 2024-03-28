using UnityEngine;

public interface IStateMachine
{
    public abstract void Enter();

    public abstract void Update();

    public abstract void FixedUpdate();

    public abstract void Exit();
}
