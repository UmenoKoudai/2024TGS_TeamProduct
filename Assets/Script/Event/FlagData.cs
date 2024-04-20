using UnityEngine;

/// <summary>
/// 各フラグのScriptableObject
/// </summary>
/// 参照サイト：https://kurokumasoft.com/2022/04/28/unity-flag-management/#toc1

[CreateAssetMenu(fileName = "New FlagData", menuName = "ScriptableObject/Flags/FlagData")]
public  class FlagData : ScriptableObject
{
    [SerializeField]
    bool _isOn = false;

    public bool IsOn =>_isOn;

    /// <summary> フラグの初期化 </summary>
    public void InitFlag()
    {
        _isOn = false;
    }

    /// <summary> フラグを切り替える。引数なしでtrueになる。 </summary>
    /// <param name="value"></param>
    public void SetFlagStatus(bool value = true)
    {
        _isOn = value;
    }

}