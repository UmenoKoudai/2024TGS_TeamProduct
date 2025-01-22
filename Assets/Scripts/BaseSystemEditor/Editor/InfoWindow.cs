using UnityEngine;
using UnityEditor;
using FoundationUtility;

public class InfoWindow : EditorWindow
{
    [MenuItem("VTNTools/InfoWindow")]
    static void CreateWindow()
    {
        InfoWindow window = (InfoWindow)EditorWindow.GetWindow(typeof(InfoWindow));
        window.Show();
        window.Init();
    }

    private string _unityVersion = "";
    private string _gitCommitHash = "";
    private string _gitRemoteUri = "";
    private string _baseSystemVersion = "";
    private GitStatusInfo _gitStatus = new GitStatusInfo();
    private string[] _gitLog = new string[] { "" };

    void Init()
    {
        Refresh();

        //エラーになるので閉じておく
        //FoundationUtility.Utility.DxDiag.GetStandardOutputFromProcess();
    }

    void Refresh()
    {
        _baseSystemVersion = FoundationUtility.Utility.Version;
        _unityVersion = FoundationUtility.Utility.GetUnityVersion();
        _gitCommitHash = FoundationUtility.Utility.GitUtility.GetCommitHash();
        _gitRemoteUri = FoundationUtility.Utility.GitUtility.GetRemoteURL();
        _gitStatus = FoundationUtility.Utility.GitUtility.GetStatus();
        _gitLog = FoundationUtility.Utility.GitUtility.GetLog();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("この画面を選択して Alt + PrintScreen で画面をコピーできます。");
        EditorGUILayout.Space(10);

        //
        if (GUILayout.Button("情報更新", GUILayout.Width(200)))
        {
            Refresh();
        }
        EditorGUILayout.Space(10);


        //Unity関係の情報
        EditorGUILayout.LabelField("Unity Infos:");
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("BaseSystem Version:" + _baseSystemVersion);
        EditorGUILayout.LabelField("Unity Version:" + _unityVersion);
        EditorGUILayout.Space(10);
        EditorGUI.indentLevel--;

        //Git関係の情報
        EditorGUILayout.LabelField("Git Status Infos:");
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("branch:" + _gitStatus.Branch);
        EditorGUILayout.LabelField("changes:" + _gitStatus.Files.Count);
        EditorGUILayout.LabelField("log:");
        foreach (var log in _gitLog)
        {
            EditorGUILayout.LabelField(log);
        }
        if (GUILayout.Button("Githubで最新コミットを差分確認", GUILayout.Width(200)))
        {
            Application.OpenURL(_gitRemoteUri + "/commit/" + _gitCommitHash);
        }
        EditorGUI.indentLevel--;
    }
}