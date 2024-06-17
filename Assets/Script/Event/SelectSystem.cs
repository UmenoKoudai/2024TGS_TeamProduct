using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSystem : MonoBehaviour
{
    [Header("SelectPanelのButtonText")]

    [Tooltip("選択肢の中の行動にうつす方")]
    [SerializeField]
    Text _yesButtonText;

    [Tooltip("選択肢の中の行動にうつす方")]
    [SerializeField]
    Text _noButtonText;

    bool _isYes;

    bool _isSelecting;

    public bool IsSelecting => _isSelecting;

    public bool IsYes => _isYes;

    /// <summary>ボタンのText設定</summary>
    /// <param name="yes">行動に移す</param>
    /// <param name="no">行動に移さない</param>
    public void SetButtonText(string yes, string no)
    {
        _yesButtonText.text = " " + yes;
        _noButtonText.text = " " + no;   
        _isSelecting = true;
    }

    /// <summary>行動するか、しないか</summary>
    ///<param name="isYes">行動する</param>
    public void YesOrNoSelect(bool isYes)
    {
        _isSelecting = false;
        _isYes = isYes;
    }
}
