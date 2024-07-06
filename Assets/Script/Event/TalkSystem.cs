using DG.Tweening;
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
    [SerializeField, Tooltip("会話Text表示速度")] float _talkTextDisplaySpeed = 0.1f;
    [SerializeField, Tooltip("次の会話にいける時のアイコン")] Image _nextTalkIcon = default;
    [SerializeField] GameManager _manager;

    EventTalkData _eventTalkData;

    TalkData[] _talkData;

    /// <summary>現在のTalk内容</summary>
    TalkData _currentTalkData;

    /// <summary>1つの会話で使う文章の配列</summary>
    string[] _talkMessageArray = null;

    /// <summary>何個目の会話か保管する変数</summary>
    int _talkCount = 0;

    /// <summary>話し中かどうか</summary>
    bool _isTalking = false;

    /// <summary>選択肢込みの会話イベントかどうか</summary>
    bool _isSelectEventTalk = false;

    /// <summary>会話のテキストを表示かどうか</summary>
    bool _isTalkTextDisplaying = false;  

    /// <summary>会話中</summary>
    public bool IsTalking => _isTalking;

    /// <summary>選択肢込みの会話イベントかどうか</summary>
    public bool IsSelectEventTalk => _isSelectEventTalk;

    /// <summary>会話のテキストを表示かどうか</summary>
    public bool IsTalkTextDisplaying => _isTalkTextDisplaying;

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
        if(eventTalkData == null) return;

        _eventTalkData = eventTalkData;
        _isSelectEventTalk = eventTalkData.IsSelectTalk;
        _talkData = _eventTalkData.EventStartTalk;
        
        //会話があるかどうかチェック
        if (_talkData == null || _talkData.Length == 0)
        {
            _isTalking = false;
            _isSelectEventTalk = false;
            return;
        }

        //会話データがあったら会話状態に入る
        _manager.StateChange(GameManager.SystemState.Talk);
        _talkCount = 0;
        OnUpdateMessage();
    }

    /// <summary>Textの文章の更新</summary>
    public void OnUpdateMessage()
    {
        _nextTalkIcon.enabled = false;

        if (_talkCount == _talkData.Length)
        {
            _talkMessageArray = null;
            _talkCount = 0;
            _isTalking = false;
            _isTalkTextDisplaying = false;
            return;
        }

        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.NextTalkTextDisplay);

        _currentTalkData = _talkData[_talkCount];

        if(_currentTalkData.Image != null)
        {
            _charaImage.sprite = _currentTalkData.Image;
            _charaImage.enabled = true;
        }
        else
        {
            _charaImage.enabled = false;
        }

        if (_currentTalkData.Name != null || _currentTalkData.Name != "")
        {
            _charaName.text = _currentTalkData.Name;
        }
        else
        {
            _charaName.text = "";
        }

        _isTalkTextDisplaying = true;

        TalkTextViewSet(_currentTalkData.Sentences);

        _talkCount++;
        _isTalking = true;
    }

    /// <summary>実行するかどうか(選択肢込みの会話だったら)で話の内容を変える</summary>
    /// <param name="isYes">実行するかどうか</param>
    public void IsEventSelectTalk(bool isYes)
    {
        _talkData = isYes ? _eventTalkData.YesTalk : _eventTalkData.NoTalk;

        _isSelectEventTalk = false;

        OnUpdateMessage();
    }

    /// <summary>会話Textのセット</summary>
    public void TalkTextViewSet(string sentences)
    {
        _talkMessage.text = "";
        _talkMessage.DOText(_currentTalkData.Sentences, _currentTalkData.Sentences.Length * _talkTextDisplaySpeed)
                    .OnComplete(() =>
                    {
                        _isTalkTextDisplaying = false;
                        _nextTalkIcon.enabled = true;     //Talk終了アイコン表示
                    });
    }

    /// <summary>会話Textを一気に表示する</summary>
    public void TalkTextAllDisplay()
    {
        //Tween中なら
        if (DOTween.IsTweening(_talkMessage))
        {
            _talkMessage.DOKill();      //Tween終了
            _talkMessage.text = _currentTalkData.Sentences;　//文字を一気に表示
            _isTalkTextDisplaying = false;      
            _nextTalkIcon.enabled = true;   　//Talk終了アイコン表示
        }
    }
}
