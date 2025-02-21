using UnityEngine;

public class VanConeEventButton : ChoiceButtonBase
{
    private AudioSource _audio;
    private Animator _windowAnim;
    private Animator _ghostAnim;

    WindowKnock _windowScript;

    private void Start()
    {
        _windowScript = FindObjectOfType<WindowKnock>();
        _audio = _windowScript.GetComponent<AudioSource>();
        _windowAnim = _windowScript.GetComponent<Animator>();
        _ghostAnim = _windowScript._GhostAnim;
    }

    protected override void OnClickAction()
    {
        _audio.Stop();
        _windowAnim.StopPlayback();
        _ghostAnim.gameObject.SetActive(true);
        _ghostAnim.Play("WindowGhost");
    }
}
