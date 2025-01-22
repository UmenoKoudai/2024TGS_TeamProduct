using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// ビルド前に実行する処理
/// </summary>
public class PreprocessBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0; // ビルド前処理の中での処理優先順位 (0で最高)

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log($"IPreprocessBuildWithReport.OnPreprocessBuild for {report.summary.platform} at {report.summary.outputPath}");

        //ビルドバリデーション

        /*
        //Addressables
        AddressableCheck("Assets/Scenes/SceneDependencies.asset");
        AddressableCheck("Assets/Prefabs/Review/Review.prefab");
        AddressableCheck("Assets/Prefabs/CRI.prefab");
        AddressableCheck("Assets/ThirdParty/Shapes2D/Materials/Shape.mat");
        */

        //IL2CPP
        if(PlayerSettings.GetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup).ToString() != "IL2CPP")
        {
            //書き換える
            PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, ScriptingImplementation.IL2CPP);
            Debug.LogWarning("ScriptingBackendをIL2CPPに変更しました");
        }

        //実装確認
        int implLine = 0;
        var files = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);
        foreach(var f in files)
        {
            if (f.Contains("ReviewTest.cs")) continue;
            if (f.Contains("GameEventRecorder.cs")) continue;
            if (f.Contains("PreprocessBuild.cs")) continue;

            var grep = File.ReadAllLines(f)
                .Select((s, i) => new { Index = i, Value = s })
                .Where(s => s.Value.Contains("VantanConnect.GameStart")
                            || s.Value.Contains("VantanConnect.GameEnd")
                            || s.Value.Contains("VantanConnect.SystemReset")
                            || s.Value.Contains("VantanConnect.SendEvent"));

            implLine += grep.Count();
            foreach(var g in grep)
            {
                Debug.Log(g);
            }
        }
        if(implLine == 0)
        {
            throw new BuildFailedException("バンタンコネクト機能が実装されていません");
        }
    }

    /*
    void AddressableCheck(string path)
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            throw new BuildFailedException($"そもそもAddressablesの設定がされていません。");
        }

        var guid = AssetDatabase.AssetPathToGUID(path);
        if (guid == null)
        {
            throw new BuildFailedException($"対象のアセットがありませんでした。[{path}]");
        }

        var find = settings.FindAssetEntry(guid);
        if (find == null)
        {
            throw new BuildFailedException($"Addressablesに登録されていません。[{path}]");
        }
    }
    */
}