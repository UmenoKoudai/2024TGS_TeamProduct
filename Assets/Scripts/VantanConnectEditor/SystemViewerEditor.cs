using UnityEngine;
using UnityEditor;
using VTNConnect;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// バンタンコネクト システムウインドウの値
/// </summary>
[CustomEditor(typeof(SystemViewer), true, isFallback = true)]
public class SystemViewerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SystemViewer view = target as SystemViewer;
        if (view.TestData == null) return;

        GUILayout.Space(10);
        if (GUILayout.Button(@"良いおうえん実行"))
        {
            EventData data = new EventData(EventDefine.Cheer);
            data.DataPack("Emotion", 50);
            data.DataPack("Message", "がんばれ");
            VantanConnect.SendEvent(data);
        }
        GUILayout.Space(10);
        if (GUILayout.Button(@"悪いおうえん実行"))
        {
            EventData data = new EventData(EventDefine.Cheer);
            data.DataPack("Emotion", -50);
            data.DataPack("Message", "くたばれ");
            VantanConnect.SendEvent(data);
        }
        /*
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
    }
}