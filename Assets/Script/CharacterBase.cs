using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    [SerializeField, Tooltip("キャラのスピード")]
    private float _speed;
    public float Speed => _speed;
    [SerializeField]
    private EventManager _eventManager;
    public EventManager EventManager => _eventManager;

    public Rigidbody2D Rb { get;  set; }
    public Vector2 Direction { get; set; } 
    public Animator Animator { get; set; }

    public enum State
    {
        NormalMove,
        AutoMove,
        Action,
    }

    public virtual void StateChange(State changeState)
    {
        Debug.LogError("オーバーライドしていません");
    }
}
