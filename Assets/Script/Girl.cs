using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Girl : CharacterBase
{
    private FollowSystem _follow;

    private void Start()
    {
        _follow = GetComponent<FollowSystem>();
    }

    private void Update()
    {
        if (_follow.MovingX > 0 || _follow.MovingX < 0 || _follow.MovingY > 0 || _follow.MovingY < 0)
        {
            Animator.SetFloat("X", _follow.MovingX);
            Animator.SetFloat("Y", _follow.MovingY);
        }
    }
}
