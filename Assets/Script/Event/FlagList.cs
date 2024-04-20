using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フラグを格納するリスト
/// </summary>
/// 参照サイト：https://kurokumasoft.com/2022/04/28/unity-flag-management/#toc1

[CreateAssetMenu(fileName = "New FlagList", menuName = "ScriptableObject/Flags/FlagList")]
public class FlagList : ScriptableObject
{

    [SerializeField, Header("フラグを追加するリスト")]
    List<FlagData> flags = new();

    public List<FlagData> Flags => flags;

    /// <summary> すべてのフラグを初期化(false)する </summary>
    public void InitFlags()
    {
        foreach (FlagData f in flags)
        {
            f.InitFlag();
        }
    }

    /// <summary> すべてのフラグをTrueにする。※デバッグ用 </summary>
    public void SetTrueAllFlags()
    {
        foreach (FlagData f in flags)
        {
            f.SetFlagStatus();
        }
    }


    /// <summary> 指定したフラグを変更する </summary>
    public void SetFlag(FlagData flag, bool value = true)
    {
        foreach (FlagData f in flags)
        {
            if (f == flag)
            {
                f.SetFlagStatus(value);
                Debug.Log($"{f}が{f.IsOn}になった");
                return;
            }
        }
    }

    /// <summary> 指定したフラグの状態を確認する </summary>
    public bool GetFlagStatus(FlagData flag)
    {
        foreach (FlagData f in flags)
        {
            if (f == flag)
            {
                return f.IsOn;
            }
        }

        return false;
    }



}