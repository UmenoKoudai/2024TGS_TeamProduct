using UnityEngine;
using UnityEngine.UI;

/// <summary>選択用ボタンにアタッチするスクリプト
/// (継承してクリック時の処理追加をすることができる)</summary>
public class ChoiceButtonBase : MonoBehaviour
{
    /// <summary>どのボタンを選択したかに必要なもの</summary>
    int _id;

    SelectSystem _selectSystem;

    /// <summary>アタッチしているGameObjectのButtonコンポーネント</summary>
    protected Button _button;

    /// <summary>クリック時に処理を行うメソッド</summary>
    protected virtual void OnClickAction() { }

    private void Start()
    {
        _button = GetComponent<Button>();
        //ButtonコンポーネントのOnClickイベントに登録
        _button.onClick.AddListener(OnClickAction);
        _button.onClick.AddListener(Pressed);
    }

    /// <summary>IDをセットするためのメソッド(SelectSystemの方でIDを決めてもらう)</summary>
    /// <param name="id">自分のIDとなるもの</param>
    public void SetID(int id, SelectSystem selectSystem)
    {
        _id = id;
        _selectSystem = selectSystem;
    }

    /// <summary>ボタンクリック時に呼ぶもの</summary>
    public void Pressed()
    {
        //選択し終わったことをSelectSystemに伝える
        _selectSystem.EndSelected(_id);
    }
}
