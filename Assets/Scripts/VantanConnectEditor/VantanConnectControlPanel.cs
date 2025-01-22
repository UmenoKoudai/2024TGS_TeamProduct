using UnityEngine;
using UnityEditor;
using VTNConnect;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// バンタンコネクト コントロールパネル
/// </summary>
public class VantanConnectControlPanel : EditorWindow
{
    [MenuItem("VTNTools/VantanConnectControlPanel")]
    static void CreateWindow()
    {
        var window = (VantanConnectControlPanel)EditorWindow.GetWindow(typeof(VantanConnectControlPanel));
        window.Show();
        window.Init();
    }

    SystemSaveData _saveData = null;
    UserData[] _storedUserData;

    void Init()
    {
        //エディタプレイ中とプレイ外で挙動を変化させる
        if (_saveData == null)
        {
            if (EditorApplication.isPlaying)
            {
                _saveData = VantanConnect.SystemSave;
            }
            else
            {
                //エディタの際はファイルから読む
                var systemSave = LocalData.Load<SystemSaveData>("SystemSave.json");
                if (systemSave == null)
                {
                    systemSave = new SystemSaveData();
                }
                _saveData = systemSave;
            }
        }
    }

    bool CheckParam<T>(ref T baseParam, T editorParam)
    {
        bool isDirty = !baseParam.Equals(editorParam);
        if (isDirty)
        {
            baseParam = editorParam;
        }
        return isDirty;
    }

    /// <summary>
    /// インスペクタ上で設定
    /// </summary>
    public void OnGUI()
    {
        //base.OnInspectorGUI();

        var headerStyle = new GUIStyle();
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.fontSize = 20;
        headerStyle.normal.textColor = Color.white;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("環境情報", headerStyle);
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("バンコネバージョン", BuildState.Version);
        EditorGUILayout.LabelField("ビルドハッシュ", BuildState.BuildHash);

        EditorGUILayout.Space(30);

        EditorGUILayout.LabelField("サンプル", headerStyle);
        EditorGUILayout.Space(10);
        if (GUILayout.Button("実装サンプルを開く", GUILayout.Width(200)))
        {
            System.Diagnostics.Process.Start(Application.dataPath + "/VCSampleScene/SampleGame");
        }

        EditorGUILayout.Space(30);

        //システムセーブ監視
        if (_saveData != null)
        {
            bool isDirty = false;
            EditorGUILayout.LabelField("システム設定", headerStyle);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("環境設定系");
            isDirty |= CheckParam(ref _saveData.Environment, (EnvironmentSetting)EditorGUILayout.EnumPopup("API塔の環境ターゲット", _saveData.Environment, GUILayout.Width(400)));

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("コネクト系");

            EditorGUIUtility.labelWidth = 250;
            isDirty |= CheckParam(ref _saveData.IsUseQRCode, EditorGUILayout.Toggle("コネクト処理用のQRコードを表示する", _saveData.IsUseQRCode, GUILayout.Width(400)));
            isDirty |= CheckParam(ref _saveData.IsDebugConnect, EditorGUILayout.Toggle("コネクト処理をローカル実行する", _saveData.IsDebugConnect, GUILayout.Width(400)));

            if (_saveData.IsDebugConnect)
            {
                isDirty |= CheckParam(ref _saveData.UseConnectUserId, EditorGUILayout.IntField("デバッグ用のコネクト処理に使用するテスト用ID", _saveData.UseConnectUserId, GUILayout.Width(400)));
            }

            if(isDirty)
            {
                Debug.Log("システムデータを保存");
                LocalData.Save<SystemSaveData>("SystemSave.json", _saveData);
            }
        }

    /*
    SystemViewer view = target as SystemViewer;
    if (view.TestData == null) return;
    if (GUILayout.Button(@"オリジナルイベント送信"))
    {
        EventSystem.SendEvent(view.TestData.EventId, view.TestData);
    }
    if (GUILayout.Button(@"オリジナルイベント実行"))
    {
        EventSystem.RunEvent(view.TestData);
    }

    GUILayout.Space(50);

    if (GUILayout.Button(@"API: サンプルソースを開く"))
    {
        System.Diagnostics.Process.Start(Application.dataPath + "/Scripts/VantanConnectEditor/EventSystemViewerEditor.cs");
    }

    ////////////////////////////////////////////////////////////
    // ここからAPIのサンプル
    // NOTE: RunOnThreadPoolは本実装では使用しないこと。

    //ユーザ取得のAPI
    if (GUILayout.Button(@"API: GetUser実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            int userId = 1;
            var result = await GameAPI.GetUser(userId);
            Debug.Log($"Status: {result.Status}");
            Debug.Log($"UserData: {JsonUtility.ToJson(result.UserData)}");
        }).Forget();
    }

    //ゲーム情報取得のAPI
    if (GUILayout.Button(@"API: GetGameUser実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            var result = await GameAPI.GetActiveGameUsers();
            Debug.Log($"Status: {result.Status}");
        }).Forget();
    }

#if !AIGAME_IMPLEMENT
    //ゲームスタートのAPI
    //NOTE: 情報を保存する
    if (GUILayout.Button(@"API: GameStart実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            int userId = 0; //コネクトしたユーザを渡す
            var result = await GameAPI.GameStart(userId);
            Debug.Log($"Status: {result.Status}");
            Debug.Log($"GameHash: {result.GameHash}");
            Debug.Log($"GameInfo: {result.GameInfo}");
        }).Forget();
    }

    //ゲーム終了のAPI
    //NOTE: GameStartを呼び出していないとエラー
    if (GUILayout.Button(@"API: GameEnd実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            var result = await GameAPI.GameEnd(true);
            Debug.Log($"Status: {result.Status}");
        }).Forget();
    }

#else

    //ゲームスタートのAPI
    //NOTE: 通常ゲームとは引数が異なる
    if (GUILayout.Button(@"API: (AIGAME)GameStart実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            var result = await GameAPI.GameStartAIGame();
            Debug.Log($"Status: {result.Status}");
            _storedUserData = result.GameUsers;
        }).Forget();
    }

    //ゲーム終了のAPI
    //NOTE: 情報格納用の関数を実行してからEndを実行する
    if (GUILayout.Button(@"API: (AIGAME)GameEnd実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            System.Random rnd = new System.Random();
            //各ユーザの結果をAPIに反映する
            foreach (var data in _storedUserData)
            {
                GameAPI.StoreUserResult(data.UserId, rnd.Next(0, 2) == 0, rnd.Next(0, 2) == 0);
            }

            //データ設定後、APIを実行する
            var result = await GameAPI.GameEndAIGame();
        }).Forget();
    }
#endif
    */
    /*
    //特に必要ないのでコメントアウトしているコード
    if (GUILayout.Button(@"API: GetAddress実行"))
    {
        UniTask.RunOnThreadPool(async () =>
        {
            Debug.Log(await GameAPI.GetAddress());
        }).Forget();
    }
    */
}
}