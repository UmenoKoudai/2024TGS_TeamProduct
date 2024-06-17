using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// シナリオや画像等、イベント関係の処理に使用するデータを格納するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "New EventData", menuName = "ScriptableObject/Events/EventData")]

public class EventData : ScriptableObject
{
    [SerializeField, Tooltip("調べたいフラグ")] FlagData _checkFlag = null;
    [SerializeField, Tooltip("変化させたいフラグ")] FlagData _changeFlag = null;
    //[Header("フラグがtrueだったら表示したい画像,名前,文章")]
    //[Tooltip("フラグがtrueだったら表示したい画像")][SerializeField] Sprite _trueImage = default;
    //[Tooltip("フラグがtrueだったら表示したい名前")][SerializeField] string _trueName = "";
    //[Tooltip("フラグがtrueだったら表示したい文章")][SerializeField] string[] _trueText;
    //[SerializeField, Tooltip("フラグがtrueだった時の会話の何番目を選択状態にするか")] int _numConversationSelect = -1;
    //[SerializeField, Tooltip("選択肢のYesの文章")] string _yesSelectText;
    //[SerializeField, Tooltip("選択肢のNoの文章")] string _noSelectText;

    //[Header("フラグがfalseだったら表示したい画像,名前,文章")]
    //[Tooltip("フラグがfalseだったら表示したい画像")][SerializeField] Sprite _falseImage = default;
    //[Tooltip("フラグがfalseだったら表示したい名前")][SerializeField] string _falseName = "";
    //[Tooltip("フラグがfalseだったら表示したい文章")][SerializeField] string[] _falseText;

    //クドウ追記
    [Header("フラグがtrueだったら表示したい画像,名前,文章")]
    [SerializeField]
    EventTalkData _trueTalkData;

    [Header("フラグがfalseだったら表示したい画像,名前,文章")]
    [SerializeField]
    EventTalkData _falseTalkData;



    public FlagData CheckFlag => _checkFlag;
    public FlagData ChangeFlag => _changeFlag;
    //public Sprite TrueImage => _trueImage;
    //public string TrueName => _trueName;
    //public string[] TrueText => _trueText;
    //public Sprite FalseImage => _falseImage;
    //public string FalseName => _falseName;
    //public string[] FalseText => _falseText;
    //public int TrueNumConversationSelectPanel => _numConversationSelect;
    //public string YesSelectText => _yesSelectText;
    //public string NoSelectText => _noSelectText;

    //クドウ追記
    public EventTalkData TrueTalkData => _trueTalkData;
    public EventTalkData FalseTalkData => _falseTalkData;
}

//クドウ追記
[Serializable]
public class TalkData
{
    [Header("話す時の設定")]

    [Tooltip("話すものの画像")]
    [SerializeField]
    Sprite _image;

    [Tooltip("話すものの名前")]
    [SerializeField]
    string _name;

    [Tooltip("話す内容")]
    [SerializeField]
    string[] _sentence;

    public Sprite Image => _image;
    public string Name => _name; 
    public string[] Sentences => _sentence;
}

//クドウ追記
[Serializable]
public class EventTalkData
{
    [Header("選択肢込みの話かどうか")]
    [SerializeField]
    bool _isSelectTalk;

    [SerializeField, EnabledIf("_isSelectTalk", false)]
    TalkData _normalTalk;

    [Tooltip("イベントが始まる前の文章")]
    [SerializeField, EnabledIf("_isSelectTalk", true)]
    TalkData _eventStartTalk;

    [Tooltip("実行する側の文章")]
    [SerializeField, EnabledIf("_isSelectTalk", true)]
    string _yesSelectText;

    [Tooltip("実行しない側の文章")]
    [SerializeField, EnabledIf("_isSelectTalk", true)]
    string _noSelectText;

    [Tooltip("実行した場合の文章")]
    [SerializeField, EnabledIf("_isSelectTalk", true)]
    TalkData _yesTalk;

    [Tooltip("実行しなかった場合の文章")]
    [SerializeField, EnabledIf("_isSelectTalk", true)]
    TalkData _noTalk;

    public bool IsSelectTalk  => _isSelectTalk;
    public TalkData NormalTalk => _normalTalk;
    public TalkData EventStartTalk => _eventStartTalk;
    public string NoSelectText => _noSelectText;
    public string YesSelectText => _yesSelectText;
    public TalkData NoTalk => _noTalk;
    public TalkData YesTalk => _yesTalk;
}
