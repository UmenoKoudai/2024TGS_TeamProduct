using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ダイアログUIに反映する
/// </summary>
public class TalkSystem : MonoBehaviour
{
    [SerializeField, Tooltip("キャラクター名")] Text _charaName = default;
    [SerializeField, Tooltip("表示する文章")] Text _talkMessage = default;
    [SerializeField, Tooltip("キャラクター画像")] Image _charaImage = default;
    [SerializeField] GameManager _manager;

    EventTalkData _eventTalkData;

    /// <summary>1つの会話で使う文章の配列</summary>
    string[] _talkMessageArray = null;

    /// <summary>何個目の会話か保管する変数</summary>
    int _talkMessagesCount = 0;

    /// <summary>話し中かどうか</summary>
    bool _isTalking = false;

    /// <summary>選択肢込みの会話イベントかどうか</summary>
    bool _isSelectEventTalk = false;

    public bool IsTalking => _isTalking;

    public bool IsSelectEventTalk => _isSelectEventTalk;

    /// <summary>
    /// ダイアログUIに反映する
    /// </summary>
    /// <param name="nextCharaName"></param>
    /// <param name="nextMessages"></param>
    /// <param name="nextCharaImage"></param>
    /// <param name="numConversationSelect"></param>
    public void ShowMessage(string nextCharaName, string[] nextMessages, Sprite nextCharaImage, bool isSelectEvent)
    {
        _manager.State = GameManager.SystemState.Talk;
        _charaName.text = nextCharaName;
        _charaImage.sprite = nextCharaImage;
        //_talkMessage.text = nextMessages; //あとでDotweenにする
        _talkMessageArray = nextMessages;
        OnUpdateMessage();
    }

    //↓クドウ追記
    /// <summary>選択肢込みの会話かどうか判断しUIに反映</summary>
    public void ShowMessage(EventTalkData eventTalkData)
    {
        _manager.State = GameManager.SystemState.Talk;
        _eventTalkData = eventTalkData;
        _isSelectEventTalk = eventTalkData.IsSelectTalk;
        TalkData talkData = _isSelectEventTalk ? _eventTalkData.EventStartTalk : _eventTalkData.NormalTalk;

        //会話更新
        _talkMessageArray = talkData.Sentences;
        TalkPanelViewSet(talkData.Image, talkData.Name);
        OnUpdateMessage();
    }

    /// <summary>Textの文章の更新</summary>
    public void OnUpdateMessage()
    {
        if (_talkMessagesCount == _talkMessageArray.Length)
        {
            _talkMessageArray = null;
            _talkMessagesCount = 0;
            _isTalking = false;
            return;
        }

        _talkMessage.text = _talkMessageArray[_talkMessagesCount];
        _talkMessagesCount++;
        _isTalking = true;
    }

    /// <summary>実行するかどうか(選択肢込みの会話だったら)で話の内容を変える</summary>
    /// <param name="isYes">実行するかどうか</param>
    public void IsEventSelectTalk(bool isYes)
    {
        TalkData talkData = isYes ? _eventTalkData.YesTalk : _eventTalkData.NoTalk;

        _talkMessageArray = talkData.Sentences;

        _isSelectEventTalk = false;

        TalkPanelViewSet(talkData.Image, talkData.Name);

        OnUpdateMessage();
    }

    /// <summary>Talkパネルの画像と名前の反映</summary>
    /// <param name="image">話すもの画像</param>
    /// <param name="name">話すものの名前</param>
    public void TalkPanelViewSet(Sprite image, string name)
    {
        _charaName.text = name;
        _charaImage.sprite = image;
    }

}
