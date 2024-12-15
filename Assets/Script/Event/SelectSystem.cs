using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSystem : MonoBehaviour
{
    [Header("SelectPanel")]

    [SerializeField]
    GameObject _selectPanel;

    List<GameObject> _choiceButtons = new List<GameObject>();

    bool _isSelecting;

    /// <summary> 選択したボタンのID</summary>
    int _selectedButtonID = -1;

    public int SelectedButtonID => _selectedButtonID;

    /// <summary>今選択中かどうか</summary>
    public bool IsSelecting { get => _isSelecting; set => _isSelecting = value; }

    /// <summary>選択肢ボタンの生成とID設定 </summary>
    /// <param name="button">生成するボタンPrefab</param>
    /// <param name="choiceButtonText">ボタンのTextに表示する文字</param>
    public void SetButton(GameObject button, string choiceButtonText)
    {
        //ボタン生成/文字設定
        GameObject choiceButton = Instantiate(button);
        choiceButton.transform.GetChild(0).GetComponent<Text>().text = choiceButtonText;
        choiceButton.transform.parent = _selectPanel.transform;
        _choiceButtons.Add(choiceButton);

        //ボタンのID設定
        ChoiceButtonBase buttonBase = choiceButton.GetComponent<ChoiceButtonBase>();

        if (buttonBase == null)
            Debug.LogError("選択肢となるボタンPrefabに　ChoiceButtonBase.cs をアタッチしてください");

        buttonBase.SetID(_choiceButtons.Count - 1, this);
    }

    /// <summary>選択肢ボタンの削除</summary>
    public void DestroyChoiceButtons()
    {
        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            Destroy(_choiceButtons[i]);
        }
        _choiceButtons.Clear();
    }

    /// <summary>選択終了</summary>
    /// <param name="id">選択したボタンのID</param>
    public void EndSelected(int id)
    {
        _selectedButtonID = id;
        _isSelecting = false;       //選択終了
        DestroyChoiceButtons();  
    }
}
