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

    /// <summary>
    /// ダイアログUIに反映する
    /// </summary>
    /// <param name="nextCharaName"></param>
    /// <param name="nextMessage"></param>
    /// <param name="nextCharaImage"></param>
    public void ShowMessage(string nextCharaName, string nextMessage, Sprite nextCharaImage)
    {
        _charaName.text = nextCharaName;
        _charaImage.sprite = nextCharaImage;
        _talkMessage.text = nextMessage; //あとでDotweenにする
    }

}
