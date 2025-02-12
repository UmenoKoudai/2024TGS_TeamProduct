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
        Animator.SetFloat("Forward_X", _follow.ForwardX);
        Animator.SetFloat("Forward_Y", _follow.ForwardY);
        Animator.SetFloat("X", _follow.MovingX);
        Animator.SetFloat("Y", _follow.MovingY);
    }
}
