using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// シナリオや画像等、イベント関係の処理に使用するデータを格納するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "New EventData", menuName = "ScriptableObject/Events/EventData")]
[Serializable]

public class EventData : ScriptableObject
{
    [SerializeField, Tooltip("調べたいフラグ")] FlagData _checkFlag = null;
    [SerializeField, Tooltip("変化させたいフラグ")] FlagData _changeFlag = null;

    //クドウ追記
    [HideInInspector, SerializeField]
    EventTalkData _trueTalkData = new();
    [HideInInspector, SerializeField]
    EventTalkData _falseTalkData = new();

    public FlagData CheckFlag => _checkFlag;
    public FlagData ChangeFlag => _changeFlag;

    //クドウ追記
    public EventTalkData TrueTalkData { get => _trueTalkData; set => _trueTalkData = value; }
    public EventTalkData FalseTalkData => _falseTalkData;

    public event UnityAction<EventData> talkEnded;

    public void TalkEnd(EventData data)
    {
        if (talkEnded != null)
        {
            talkEnded(data);
        }
    }

}

//クドウ追記
[Serializable]
public class TalkData
{
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public string name = "";
    [SerializeField]
    public string sentence = "";

    public Sprite Image { get => image; set => image = value; }
    public string Name { get => name; set => name = value; }
    public string Sentences => sentence;
}

//クドウ追記
[Serializable]
public class EventTalkData
{
    /// <summary>選択肢込みの会話かどうか</summary>
    [SerializeField]
    public bool isSelectTalk = false;

    /// <summary>選択イベントが始まる前の会話</summary>
    [SerializeField]
    public TalkData[] eventStartTalk;

    [SerializeField]
    public int choiceNum;

    [SerializeField]
    public ChoiceButtonData[] choiceButtonDatas;

    /// <summary>選択肢込みの会話かどうか</summary>
    public bool IsSelectTalk => isSelectTalk;
    /// <summary>選択イベントが始まる前の会話</summary>
    public TalkData[] EventStartTalk => eventStartTalk;

    public ChoiceButtonData[] ChoiceButtonData => choiceButtonDatas;

}

[Serializable]
public class ChoiceButtonData
{
    [SerializeField]
    public GameObject button;

    [SerializeField]
    public string buttonText = "";

    [SerializeField]
    public TalkData[] talk = new TalkData[0];
}

