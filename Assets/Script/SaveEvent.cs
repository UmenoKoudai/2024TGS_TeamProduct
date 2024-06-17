using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEvent : MonoBehaviour, IEventObject, IAction
{
    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;
    [SerializeField] SavePoint _save;
    public EventData EventData => _eventData;
    public EventTalkData ResultEventTalkData { get; set; }

    public void Execute(CharacterBase chara)
    {
        Debug.Log("SaveAction");
        SaveLoadManager.Instance.SaveAction();
    }

    public void ResultFlagCheck()
    {
        Debug.Log("Save");
        GameManager.Instance.StateChange(GameManager.SystemState.Talk);
        ResultEventTalkData = _eventData.TrueTalkData;
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
