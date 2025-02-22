using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _talkPanel;
    public GameObject TalkPanel => _talkPanel;
    [SerializeField]
    private GameObject _selectPanel;
    public GameObject SelectPanel => _selectPanel;
    private GameObject _optionPanel;
    public GameObject OptionPanel
    {
        get
        {
            _optionPanel = FindObjectOfType<ItemPanel>().Panel;
            return _optionPanel;
        }
    }
}
