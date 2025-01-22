using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.CodeDom.Compiler;

public class PackageExporter
{
    /// <summary>
    /// パッケージ対象のフォルダ、ファイル
    /// </summary>
    static string[] PackTargets = new string[] {
        "Assets/ConnectEventAssets",
        "Assets/Resources",
        "Assets/Scripts",
        "Assets/ThirdParty",
        "Assets/VCSampleScene",
    };

    /// <summary>
    /// バージョニング
    /// </summary>
    [MenuItem("VTNTools/Export/Versioning %e")]
    static void Versioning()
    {
        string PackageName = Application.productName;
        string Version = Application.version;

        //ビルド情報を記載したファイルを自動生成
        BuildStateBuild(PackageName, Version);
    }

    /// <summary>
    /// 自動でパッケージ化する
    /// </summary>
    [MenuItem("VTNTools/Export/Export Packages %e")]
    static void Packaging()
    {
        string PackageName = Application.productName;
        string Version = Application.version;
        string ProjectPath = Application.dataPath.Replace("/Assets", "/ExportPackage");
        string fileName = string.Format("{0}/{1}_{2}.unitypackage", ProjectPath, PackageName, Version);

        Directory.CreateDirectory(ProjectPath);

        AssetDatabase.ExportPackage(PackTargets, fileName, ExportPackageOptions.Recurse);

        Debug.Log("Export:" + fileName);
    }


    const string targetPath = "Assets/Scripts/BaseSystem/Dynamic";
    const string source = @"
public class BuildState
{
    const string _hash = ""<Hash>"";
    const string _project = ""<Project>"";
    public const string Version = ""<Version>"";

    public static string BuildHash
    {
        get
        {
            return _hash;
        }
    }
};";

    static public void BuildStateBuild(string project, string version)
    {
        Directory.CreateDirectory(targetPath);

        //動的生成
        using (FileStream fs = new FileStream(targetPath + "/BuildState.cs", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
        {
            string sourceCode = source;
            sourceCode = sourceCode.Replace("<Hash>", Guid.NewGuid().ToString()); //ビルドハッシュを新規生成する
            sourceCode = sourceCode.Replace("<Project>", project); //ライブラリバージョン
            sourceCode = sourceCode.Replace("<Version>", version); //ライブラリバージョン
            byte[] bytes = Encoding.UTF8.GetBytes(sourceCode);
            fs.Write(bytes, 0, bytes.Length);
        }
    }
}