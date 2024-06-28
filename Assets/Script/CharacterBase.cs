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
    [SerializeField, Tooltip("アニメーターを設定")]
    private Animator animator;

    public Tilemap Map => _groundMap;

    public EventManager Event { get; set; }

    public Rigidbody2D Rb { get;  set; }
    public Vector2 Direction { get; set; }
    public Animator Animator => animator;

    public enum State
    {
        NormalMove,
        AutoMove,
        Action,

        MacCount,
    }

    public virtual void StateChange(State changeState)
    {
        Debug.LogError("オーバーライドしていません");
    }

    public virtual void CreatePos(Vector3 pos)
    {

    }
}
