using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventData))]
public class EventDataEditor : Editor
{
    EventData _target;
    private int toolbarSelected = 0;

    private string inputString = "";
    public void OnEnable()
    {
        _target = target as EventData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("会話イベントの内容(Flag別)", EditorStyles.boldLabel);

        toolbarSelected = GUILayout.Toolbar(toolbarSelected, new[] { "True", "False"});

        if (toolbarSelected == 0)
        {
            FlagEventTalkDataView(_target.TrueTalkData);
        }
        else
        {
            FlagEventTalkDataView(_target.FalseTalkData);
        }
    }

    public void FlagEventTalkDataView(EventTalkData eventTalkData)
    {
        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField("会話の中に選択イベントがあるか", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        var isSelectTalk = EditorGUILayout.Toggle("isSelectTalk", eventTalkData.isSelectTalk);

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("会話内容", EditorStyles.boldLabel);
        TalkView(ref eventTalkData.eventStartTalk);

        if (EditorGUI.EndChangeCheck())
        {
            eventTalkData.isSelectTalk = isSelectTalk;
            Undo.RecordObject(_target, "Changed TalkSelect");
            EditorUtility.SetDirty(_target);
        }

        if (isSelectTalk)
        {
            EditorGUILayout.Space(30);
            var color = GUI.backgroundColor;
            GUI.backgroundColor = UnityEngine.Color.black;
            GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
            GUI.backgroundColor = color;
            EditorGUILayout.Space(25);

            EditorGUILayout.LabelField("選択イベント", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            inputString = eventTalkData.choiceNum.ToString();

            inputString = EditorGUILayout.TextField("選択数", inputString);
            
            if (EditorGUI.EndChangeCheck())
            {
                bool isHalfWidth = IsHalfWidth(inputString);
                int choiceNum = 0;
                if (isHalfWidth &&  (int.TryParse(inputString, out choiceNum)))
                {
                    int deleteNum = eventTalkData.choiceButtonDatas.Length - choiceNum;
                    if (deleteNum > 0)
                    {
                        for (int i = deleteNum; i > 0; i--)
                        {
                            ArrayUtility.RemoveAt(ref eventTalkData.choiceButtonDatas, eventTalkData.choiceButtonDatas.Length - 1);
                        }
                        Undo.RecordObject(target, "Deleted TalkData");
                        EditorUtility.SetDirty(_target);
                    }
                    else if (deleteNum < 0)
                    {
                        deleteNum *= -1;
                        for (int i = 0; i < deleteNum; i++)
                        {
                            ChoiceButtonData choiceButtonData = new ChoiceButtonData();
                            ArrayUtility.Add(ref eventTalkData.choiceButtonDatas, choiceButtonData);
                        }
                        Undo.RecordObject(_target, "Add Talk");
                        EditorUtility.SetDirty(_target);
                    }
                    eventTalkData.choiceNum = eventTalkData.choiceButtonDatas.Length;
                }
            }

            for (int i = 0; i < eventTalkData.choiceButtonDatas.Length; i++)
            {
                EditorGUILayout.Space(30);
                GUILayout.Box("", GUILayout.Height(2), GUILayout.ExpandWidth(true));
                EditorGUILayout.LabelField($"選択肢 {i}");
                EditorGUI.BeginChangeCheck();
                GameObject button =  (GameObject)EditorGUILayout.ObjectField("ButtonPrefab", eventTalkData.choiceButtonDatas[i].button, typeof(GameObject), true);
                string choiceButtonText = EditorGUILayout.TextField("選択肢Text", eventTalkData.choiceButtonDatas[i].buttonText);
                if (EditorGUI.EndChangeCheck())
                {
                    eventTalkData.choiceButtonDatas[i].button = button;
                    eventTalkData.choiceButtonDatas[i].buttonText = choiceButtonText;
                    Undo.RecordObject(_target, "Changed ChoiceButtonText");
                    EditorUtility.SetDirty(_target);
                }
                TalkView(ref eventTalkData.choiceButtonDatas[i].talk);
            }
        }
    }

    public void TalkView(ref TalkData[] talkDataArray)
    {
        if (GUILayout.Button("TalkAdd"))
        {
            TalkData talkData = new TalkData();
            ArrayUtility.Add(ref talkDataArray, talkData);
            Undo.RecordObject(_target, "Add Talk");
            EditorUtility.SetDirty(_target);
        }

        int delete = -1;
        int size = 50;
        int index = -1;
        int changeIndex = -1;

        var style = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true
        };

        for (int i = 0; talkDataArray != null &&i < talkDataArray.Length && talkDataArray.Length > 0; i++)
        {
            EditorGUILayout.Space(10);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.Width(size));

            if (GUILayout.Button("削除", GUILayout.Width(size)))
            {
                delete = i;
            }
            EditorGUILayout.LabelField($"{i + 1}話目",GUILayout.Width(size));
            changeIndex = EditorGUILayout.IntField("", i + 1, GUILayout.Width(size));
            index = i + 1;
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            TalkData talkData = new TalkData();
            talkData.image = (Sprite)EditorGUILayout.ObjectField("画像", talkDataArray[i].image, typeof(Sprite), true, GUILayout.Height(18));
            talkData.name = EditorGUILayout.TextField("名前", talkDataArray[i].name);
            EditorGUILayout.LabelField("文章");
            talkData.sentence = EditorGUILayout.TextArea(talkDataArray[i].sentence, style, GUILayout.Height(EditorGUIUtility.singleLineHeight * 3));

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                talkDataArray[i] = talkData;
                Undo.RecordObject(_target, "Changed TalkData");
                EditorUtility.SetDirty(_target);
            }

            changeIndex--;
            index--;
            if (changeIndex >= 0 && changeIndex < talkDataArray.Length && changeIndex != index)
            {
                TalkData save = talkDataArray[index];
                if (index > changeIndex)
                {
                    for (int j = index; j >= changeIndex; j--)
                    {
                        if (j == changeIndex)
                        {
                            talkDataArray[j] = talkData;
                        }
                        else
                        {
                            talkDataArray[j] = talkDataArray[j - 1];
                        }
                    }
                }
                else if (index < changeIndex)
                {
                    for (int j = index; j <= changeIndex; j++)
                    {
                        if (j == changeIndex)
                        {
                            talkDataArray[j] = talkData;
                        }
                        else
                        {
                            talkDataArray[j] = talkDataArray[j + 1];
                        }
                    }
                }
                Undo.RecordObject(_target, "Changed TalkDataIndex");
                EditorUtility.SetDirty(_target);
            }
        }

        if (delete != -1)
        {
            ArrayUtility.RemoveAt(ref talkDataArray, delete);
            Undo.RecordObject(target, "Deleted TalkData");
            EditorUtility.SetDirty(_target);
        }

    }

    /// <summary>半角数字かどうかを判定するメソッド</summary>
    /// <param name="input">文字</param>
    /// <returns>半角数字ならTrue</returns>
    bool IsHalfWidth(string input)
    {
        foreach (char c in input)
        {
            if (c < '0' || c > '9') 
            {
                return false;
            }
        }
        return true;  
    }
}
