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
    [Header("フラグがtrueだったら表示したい画像,名前,文字")]
    [Tooltip("フラグがtrueだったら表示したい画像")][SerializeField] Sprite _trueImage = default;
    [Tooltip("フラグがtrueだったら表示したい名前")][SerializeField] string _trueName = "";
    [Tooltip("フラグがtrueだったら表示したい文字")][SerializeField] string _trueText = "";
    [Header("フラグがfalseだったら表示したい画像,名前,文字")]
    [Tooltip("フラグがfalseだったら表示したい画像")][SerializeField] Sprite _falseImage = default;
    [Tooltip("フラグがfalseだったら表示したい名前")][SerializeField] string _falseName = "";
    [Tooltip("フラグがfalseだったら表示したい文字")][SerializeField] string _falseText = "";

    public FlagData CheckFlag => _checkFlag;
    public FlagData ChangeFlag => _changeFlag;
    public Sprite TrueImage => _trueImage;
    public string TrueName => _trueName;
    public string TrueText => _trueText;
    public Sprite FalseImage => _falseImage;
    public string FalseName => _falseName;
    public string FalseText => _falseText;
}
