using Cysharp.Threading.Tasks;
using DataManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

/// <summary>
/// ローカライズテキスト表示用エディタ拡張
/// </summary>
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LocalizeTextAttribute))]
public class LocalizeTextAttributeDrawer : PropertyDrawer
{
    int _selected = 0; //選択しているテキスト

    VisualElement _element = null; //GUIまとめるコンテナ(管理オブジェクト)
    bool _isInit = false; //初期化フラグ

    /// <summary>
    /// GUI生成
    /// NOTE: 作り直すときもこれを呼び出す
    /// </summary>
    void ContainerBuild(SerializedProperty property)
    {
        if (_isInit) return;

        //GUIクリア
        _element.Clear();

        //参照するマスタ名を拾ってくる(※今は使ってない)
        var attr = attribute as LocalizeTextAttribute;
        var master = attr.Master;
        
        //設定可能なテキストのリストを作る
        List<string> list = new List<string>();
        list.Add("None");
        list = list.Concat(MasterData.TextMaster.GetKeys()).ToList();

        _selected = list.IndexOf(property.stringValue);
        if (_selected == -1) _selected = 0;

        //プレビューを作る
        var text = new TextField();
        text.isReadOnly = true;

        //テキストの選択フィールドと、選択後にプレビューを更新する処理
        bool isInit = true;
        var popup = new PopupField<string>(property.name.Replace("_",""), list, _selected, (string s) =>
        {
            //tags[idx].Index = tags[idx].TagArray.ToList().IndexOf(s);
            //BuildContainer(property);
            if (s == "None")
            {
                text.style.visibility = Visibility.Hidden;
                text.style.height = 0;
                text.value = "";

                property.stringValue = "";
                property.serializedObject.ApplyModifiedProperties();
                return s;
            }

            text.style.visibility = Visibility.Visible;
            text.style.textOverflow = TextOverflow.Ellipsis;
            text.style.alignItems = Align.Stretch;
            text.value = MasterData.TextMaster[s];
            text.MarkDirtyRepaint();

            if (isInit) return s;

            //データの値を更新して更新をマークする
            property.stringValue = s;
            property.serializedObject.ApplyModifiedProperties();
            ContainerBuild(property);

            return s;
        });

        //GUI構築
        _element.Add(popup);
        _element.Add(text);
        _element.MarkDirtyRepaint();

        isInit = false;
    }

    /// <summary>
    /// GUI初期化
    /// </summary>
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        //GUI管理オブジェクト
        _element = new VisualElement();

        //マスターデータの読み込みが終わってなければ別のUIを出して待つ
        if (!MasterData.Instance.IsSetupComplete)
        {
            //GUI再構築
            _element.Clear();
            _element.Add(new Label() { text = "マスタデータを読み込み中です…" });
            _element.Add(new Button(() =>
            {
                ContainerBuild(property);
            }) { text = "再読み込み" });
            
            //マスターデータ読み込み
            MasterData.Instance.Setup(() =>
            {
                ContainerBuild(property);
            }).Forget();

            return _element;
        }

        //マスターデータがあれば通常通りGUI構築
        ContainerBuild(property);
        return _element;
    }
}
#endif