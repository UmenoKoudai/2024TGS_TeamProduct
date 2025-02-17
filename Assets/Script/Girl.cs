using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Girl : CharacterBase
{
    private FollowSystem _follow;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _follow = GetComponent<FollowSystem>();
    }

    private void Update()
    {
        if(_follow.Target.position.y > this.transform.position.y)
        {
            _spriteRenderer.sortingOrder = 5;
        }
        else
        {
            _spriteRenderer.sortingOrder = -1;
        }
        Animator.SetFloat("Forward_X", _follow.ForwardX);
        Animator.SetFloat("Forward_Y", _follow.ForwardY);
        Animator.SetFloat("X", _follow.MovingX);
        Animator.SetFloat("Y", _follow.MovingY);
    }
}
