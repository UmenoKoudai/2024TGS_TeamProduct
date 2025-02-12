using UnityEngine;
using UnityEditor;
using VTNConnect;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor.Overlays;

/// <summary>
/// バンタンコネクト コントロールパネル
/// </summary>
public class VantanConnectSetup : EditorWindow
{
    [MenuItem("VTNTools/VantanConnectSetup")]
    static void CreateWindow()
    {
        var window = (VantanConnectSetup)EditorWindow.GetWindow(typeof(VantanConnectSetup));
        window.Show();
        window.Init();
    }

    const string BaseURL = "https://script.google.com/macros/s/AKfycbwxrmXMYPwuDnCTugeCQ3XYUeNv54FeFQG7h_nPmnQBj7zDj_qRibqklzxZPu__NydlSQ/exec";
    bool _isSetup = false;
    bool _isSetupDone = false;

    [Serializable]
    class GameInfoData
    {
        public int Id;
        public string ProjectCode;
        public string GameTitle;
        public int Difficulty;
        public int ClearBonusCoin;
        public int GameOverBonusCoin;
        public int PlayTime;
    }

    [Serializable]
    class GameEventData
    {
        public int Id;
        public string Description;
        public int From;
        public int Target;
        public string SendFlag;
        public string EnumCode;
        public string Release;
        public string System;
    }

    [Serializable]
    class GameInfoMaster
    {
        public int Version;
        public GameInfoData[] Data;
    }

    [Serializable]
    class GameEventMaster
    {
        public int Version;
        public GameEventData[] Data;
    }

    GameInfoMaster _gameInfo;
    GameEventMaster _gameEvent;
    List<GameEventData> _filteredEventList = new List<GameEventData>();
    int _gameID = ProjectSettings.GameID;

    void Init()
    {
        UniTask.RunOnThreadPool(async () =>
        {
            var result = await Network.WebRequest.GetRequest(string.Format("{0}?sheet=GameInfo", BaseURL));
            _gameInfo = JsonUtility.FromJson<GameInfoMaster>(result);

            var result2 = await Network.WebRequest.GetRequest(string.Format("{0}?sheet=GameEvent", BaseURL));
            _gameEvent = JsonUtility.FromJson<GameEventMaster>(result2);

            _filteredEventList = _gameEvent.Data.Where(d => (d.System.Trim() == "" && d.Release != "")).ToList();
        }).ContinueWith(() =>
        {
            _isSetup = true;
        }).Forget();
    }


    string GetFrom(int from)
    {
        if (from == -1)
        {
            return "すべてのゲーム";
        }
        else if (from == 0)
        {
            return "バンコネシステム";
        }
        {
            for (int i = 0; i < _gameInfo.Data.Length; ++i)
            {
                if (_gameInfo.Data[i].Id != from) continue;
                return _gameInfo.Data[i].ProjectCode;
            }
            return "不明";
        }
    }

    string GetTarget(int target)
    {
        if (target == -1)
        {
            return "すべてのゲーム";
        }
        else
        {
            for (int i = 0; i < _gameInfo.Data.Length; ++i)
            {
                if (_gameInfo.Data[i].Id != target) continue;
                return _gameInfo.Data[i].ProjectCode;
            }
            return "不明";
        }
    }

    /// <summary>
    /// インスペクタ上で設定
    /// </summary>
    public void OnGUI()
    {
        var headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.fontSize = 20;
        headerStyle.normal.textColor = Color.white;

        if(!_isSetup)
        {
            EditorGUILayout.LabelField("データ準備中...", headerStyle);
            return;
        }

        if (_isSetupDone)
        {
            EditorGUILayout.LabelField("設定完了！ Windowを閉じて大丈夫です。", headerStyle);
            return;
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("ゲーム関連の設定", headerStyle);

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("正しいIDをゲームリストから選択して入力してください");
        _gameID = EditorGUILayout.IntField("GameId", _gameID, GUILayout.Width(300));

        var listStyle = new GUIStyle();
        listStyle.fontStyle = FontStyle.Bold;
        listStyle.alignment = TextAnchor.MiddleCenter;
        listStyle.normal.textColor = Color.white;

        EditorGUILayout.Space(50);

        var buttonStyle = new GUIStyle();
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fontSize = 36;
        buttonStyle.normal.textColor = Color.white;
        if (GUILayout.Button(@"設定", GUILayout.Width(100), GUILayout.Height(50)))
        {
            CreateSettingFile();
        }

        EditorGUILayout.Space(50);

        EditorGUILayout.LabelField("登録済みのゲームリスト");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID(これを入力)", listStyle, GUILayout.Width(100));
        GUILayout.Label("プロジェクトコード", listStyle, GUILayout.Width(100));
        GUILayout.Label("ゲーム名", listStyle, GUILayout.Width(100));
        GUILayout.Label("難易度", listStyle, GUILayout.Width(100));
        GUILayout.Label("獲得コイン(クリア)", listStyle, GUILayout.Width(120));
        GUILayout.Label("獲得コイン(ゲームオーバー)", listStyle, GUILayout.Width(150));
        GUILayout.Label("予想プレイ時間", listStyle, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        listStyle.fontStyle = FontStyle.Normal;

        for (int i=0; i<_gameInfo.Data.Length; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(_gameInfo.Data[i].Id.ToString(), listStyle, GUILayout.Width(100));
            GUILayout.Label(_gameInfo.Data[i].ProjectCode, listStyle, GUILayout.Width(100));
            GUILayout.Label(_gameInfo.Data[i].GameTitle, listStyle, GUILayout.Width(100));
            GUILayout.Label(_gameInfo.Data[i].Difficulty.ToString(), listStyle, GUILayout.Width(100));
            GUILayout.Label(_gameInfo.Data[i].ClearBonusCoin.ToString(), listStyle, GUILayout.Width(120));
            GUILayout.Label(_gameInfo.Data[i].GameOverBonusCoin.ToString(), listStyle, GUILayout.Width(150));
            GUILayout.Label(_gameInfo.Data[i].PlayTime.ToString(), listStyle, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(50);

        EditorGUILayout.LabelField("登録済みのゲームイベント");
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID", listStyle, GUILayout.Width(100));
        GUILayout.Label("なにするイベント？", listStyle, GUILayout.Width(250));
        GUILayout.Label("どのゲームからくるか", listStyle, GUILayout.Width(100));
        GUILayout.Label("どのゲームにとぶか", listStyle, GUILayout.Width(100));
        GUILayout.Label("対象", listStyle, GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();

        listStyle.fontStyle = FontStyle.Normal;

        for (int i = 0; i < _filteredEventList.Count; ++i)
        {
            if (_filteredEventList[i].Description.Trim() == "") continue;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(_filteredEventList[i].Id.ToString(), listStyle, GUILayout.Width(100));
            GUILayout.Label(_filteredEventList[i].Description, listStyle, GUILayout.Width(250));
            GUILayout.Label(GetFrom(_filteredEventList[i].From), listStyle, GUILayout.Width(100));
            GUILayout.Label(GetTarget(_filteredEventList[i].Target), listStyle, GUILayout.Width(100));
            GUILayout.Label(_filteredEventList[i].SendFlag, listStyle, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();
        }
    }




    const string targetPath = "Assets/Scripts/VantanConnect/Dynamic";
    const string source = @"
//動的生成されます
namespace VTNConnect
{
    public class ProjectSettings
    {
        public const int GameID = <GameID>;
    }
}";

    const string eventDefSource = @"
//これはコードで自動生成されます
//みんなが使用できるイベントのみが表示されます
namespace VTNConnect
{
    public enum EventDefine
    {
<EventList>
    }
}
";

    const string episodeDefSource = @"
//これはコードで自動生成されます
//みんなが使用できるイベントのみが表示されます
namespace VTNConnect
{
    public enum EpisodeCode
    {
<EpisodeList>
    }
}
";


    string EventDefineCreate(List<GameEventData> data)
    {
        string ret = "";
        foreach(var d in data)
        {
            ret += $"        {d.EnumCode} = {d.Id},  //{d.Description} ({GetFrom(d.From)} -> {GetFrom(d.Target)}、{d.SendFlag})\n";
        }
        return ret;
    }
    string EpisodeDefineCreate(List<GameEventData> data)
    {
        string ret = "";
        foreach (var d in data)
        {
            ret += $"        {d.EnumCode} = {d.Id},  //{d.Description}\n";
        }
        return ret;
    }

    void CreateSettingFile()
    {
        Directory.CreateDirectory(targetPath);

        //動的生成
        using (FileStream fs = new FileStream(targetPath + "/ProjectSettings.cs", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            string sourceCode = source;
            sourceCode = sourceCode.Replace("<GameID>", _gameID.ToString()); //GameID置き換え
            byte[] bytes = Encoding.UTF8.GetBytes(sourceCode);
            fs.Write(bytes, 0, bytes.Length);
        }

        using (FileStream fs = new FileStream(targetPath + "/EventDefine.cs", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            var events = _filteredEventList.Where(d => (_gameID == 100 || (d.From == _gameID || d.From == -1 || d.From == 100) || (d.Target == _gameID || d.Target == -1 || d.Target == 100)) && (d.Id < 200 || d.Id >= 1000)).ToList();

            string sourceCode = eventDefSource;
            sourceCode = sourceCode.Replace("<EventList>", EventDefineCreate(events));      //イベント置き換え

            byte[] bytes = Encoding.UTF8.GetBytes(sourceCode);
            fs.Write(bytes, 0, bytes.Length);
        }

        using (FileStream fs = new FileStream(targetPath + "/EpisodeCode.cs", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            var episodes = _filteredEventList.Where(d => (_gameID == 100 || (d.From == _gameID || d.From == -1 || d.From == 100) || (d.Target == _gameID || d.Target == -1 || d.Target == 100)) && (d.Id >= 200 && d.Id < 1000)).ToList();
            
            string sourceCode = episodeDefSource;
            sourceCode = sourceCode.Replace("<EpisodeList>", EpisodeDefineCreate(episodes));      //イベント置き換え

            byte[] bytes = Encoding.UTF8.GetBytes(sourceCode);
            fs.Write(bytes, 0, bytes.Length);
        }

        var systemSave = LocalData.Load<SystemSaveData>("SystemSave.json");
        if (systemSave != null)
        {
            systemSave.GameID = _gameID;
            LocalData.Save<SystemSaveData>("SystemSave.json", systemSave);
        }

        _isSetupDone = true;
    }
}