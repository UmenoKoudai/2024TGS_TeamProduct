using UnityEngine;

public class VanConeEventButton : ChoiceButtonBase
{
    private AudioSource _audio;
    private Animator _windowAnim;
    private Animator _ghostAnim;

    WindowKnock _windowScript;

    protected override void OnClickAction()
    {
        _windowScript = FindObjectOfType<WindowKnock>();
        _audio = _windowScript.GetComponent<AudioSource>();
        _windowAnim = _windowScript.GetComponent<Animator>();
        _ghostAnim = _windowScript._GhostAnim;
        _audio.Stop();
        _windowScript.EventData.CheckFlag.SetFlagStatus(false);
        _windowAnim.StopPlayback();
        _ghostAnim.gameObject.SetActive(true);
        _ghostAnim.Play("WindowGhost");
    }
}
