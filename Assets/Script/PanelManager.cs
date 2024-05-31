using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _talkPanel;
    public GameObject TalkPanel => _talkPanel;
    [SerializeField]
    private GameObject _optionPanel;
    public GameObject OptionPanel => _optionPanel;
}
