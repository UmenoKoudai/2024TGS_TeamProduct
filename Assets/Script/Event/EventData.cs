using System;
using System.Collections.Generic;
using UnityEngine;
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

    /// <summary>実行する側の文</summary>
    [SerializeField]
    public string yesSelectText;

    /// <summary>実行しない側の文</summary>
    [SerializeField]
    public string noSelectText;

    /// <summary>実行した場合の会話</summary>
    [SerializeField]
    public TalkData[] yesTalk;

    /// <summary>実行しなかった場合の会話</summary>
    [SerializeField]
    public TalkData[] noTalk;

    /// <summary>選択肢込みの会話かどうか</summary>
    public bool IsSelectTalk => isSelectTalk;
    /// <summary>選択イベントが始まる前の会話</summary>
    public TalkData[] EventStartTalk => eventStartTalk;
    /// <summary>実行しない側の文</summary>
    public string NoSelectText => noSelectText;
    /// <summary>実行する側の文</summary>
    public string YesSelectText => yesSelectText;
    /// <summary>実行しなかった場合の会話</summary>
    public TalkData[] NoTalk => noTalk;
    /// <summary>実行した場合の会話</summary>
    public TalkData[] YesTalk => yesTalk;
}

