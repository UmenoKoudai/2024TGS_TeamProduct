using UnityEngine;

public class VanConeEventButton : ChoiceButtonBase
{
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private Animator _windowAnim;
    [SerializeField]
    private Animator _ghostAnim;
    protected override void OnClickAction()
    {
        _audio.Stop();
        _windowAnim.StopPlayback();
        _ghostAnim.gameObject.SetActive(true);
        _ghostAnim.Play("WindowGhost");
    }
}
