using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _talkPanel;
    public GameObject TalkPanel => _talkPanel;
    [SerializeField]
    private GameObject _optionPanel;
    public GameObject OptionPanel => _optionPanel;
    [SerializeField]
    private GameObject _selectPanel;
    public GameObject SelectPanel => _selectPanel;
}
