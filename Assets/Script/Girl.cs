using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Girl : CharacterBase
{
    private FollowSystem _follow;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private float _followStartTime = 1f;

    private float _timer = 0f;


    private void Start()
    {
        _follow = GetComponent<FollowSystem>();
        _timer = 0;
    }

    private void Update()
    {
        if(!_follow.IsFollow)
        {
            _timer += Time.deltaTime;
            if(_timer >= _followStartTime)
            {
                _follow.FollowStart();
            }
        }

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
