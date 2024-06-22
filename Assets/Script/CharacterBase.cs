using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBase : MonoBehaviour
{
    [SerializeField, Tooltip("�L�����̃X�s�[�h")]
    private float _speed;
    public float Speed => _speed;
    [SerializeField, Tooltip("�L�����̈ړ��\�ȃ^�C���}�b�v")]
    private Tilemap _groundMap;

    public Tilemap Map => _groundMap;

    public EventManager Event { get; set; }

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
        Debug.LogError("�I�[�o�[���C�h���Ă��܂���");
    }

    public virtual void CreatePos(Vector3 pos)
    {

    }
}
