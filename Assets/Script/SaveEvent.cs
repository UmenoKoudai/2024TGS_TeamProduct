using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEvent : MonoBehaviour, IEventObject, IAction
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] SavePoint _save;
    public EventData EventData => _eventData;

    public Sprite ResultImage { get; set; }
    public string ResultName { get; set; }
    public string ResultText { get; set; }

    public void Execute(CharacterBase chara)
    {
        Debug.Log("SaveAction");
        SaveLoadManager.Instance.SaveAction();
    }

    public void ResultFlagCheck()
    {
        Debug.Log("Save");
        GameManager.Instance.StateChange(GameManager.SystemState.Talk);
        ResultImage = _eventData.TrueImage;
        ResultName = _eventData.TrueName;
        ResultText = _eventData.TrueText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
