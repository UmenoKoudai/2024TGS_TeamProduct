using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace FoundationUtility
{
    public class FileStatus
    {
        public string Mode;
        public string FilePath;
    }
    public class GitStatusInfo
    {
        public string Branch;
        public List<FileStatus> Files = new List<FileStatus>();
    }
    public class PCInfo
    {

    }


    public class Utility
    {
        //NOTE: 手動なので何とかしたいが…
        public const string Version = "v0.1.2";

        public class GitUtility
        {
            //https://qiita.com/segur/items/b743b5e0aa070de06c96

            /// <summary>
            /// Gitのstatusを確認する。
            /// </summary>
            static public GitStatusInfo GetStatus()
            {
                // gitのパスを取得する。
                string gitPath = GetGitPath();

                // gitのコマンドを設定する。
                string gitCommand = "status -s -b";

                // コマンドを実行して標準出力を取得する。
                string outStr = GetStandardOutputFromProcess(gitPath, gitCommand).Trim();

                var lines = outStr.Split('\n').ToList();
                GitStatusInfo info = new GitStatusInfo();
                info.Branch = lines[0];
                lines.RemoveAt(0);
                for (int i = 1; i < lines.Count; ++i)
                {
                    var prms = lines[i].Split(' ');
                    info.Files.Add(new FileStatus() { FilePath = prms[1], Mode = prms[0] });
                }
                return info;
            }

            /// <summary>
            /// Gitの最新ログを確認する。
            /// </summary>
            static public string[] GetLog()
            {
                // gitのパスを取得する。
                string gitPath = GetGitPath();

                // gitのコマンドを設定する。
                string gitCommand = "log -n 1";

                // コマンドを実行して標準出力を取得する。
                string outStr = GetStandardOutputFromProcess(gitPath, gitCommand).Trim();

                var lines = outStr.Split('\n');

                return lines;
            }

            /// <summary>
            /// Git最新のコミットハッシュを確認する
            /// </summary>
            static public string GetCommitHash()
            {
                // gitのパスを取得する。
                string gitPath = GetGitPath();

                // gitのコマンドを設定する。
                string gitCommand = "show --format='%H' --no-patch";

                // コマンドを実行して標準出力を取得する。
                string hash = GetStandardOutputFromProcess(gitPath, gitCommand).Trim();
                hash = hash.Replace("'","");
                return hash;
            }

            /// <summary>
            /// GithubのプロジェクトURLを取得する(.git抜き)
            /// </summary>
            static public string GetRemoteURL()
            {
                // gitのパスを取得する。
                string gitPath = GetGitPath();

                // gitのコマンドを設定する。
                string gitCommand = "remote get-url origin";

                // コマンドを実行して標準出力を取得する。
                string uri = GetStandardOutputFromProcess(gitPath, gitCommand).Trim();

                uri = uri.Replace(".git", "");

                //sshだった場合はhttpに変換する
                return uri.Replace("git@github.com:", "https://github.com/");
            }

            /// <summary>
            /// Gitの実行ファイルのパスを取得する。
            /// </summary>
            /// <returns>Gitのパス</returns>
            static private string GetGitPath()
            {
                // Macのとき
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    // パスの候補
                    string[] exePaths =
                    {
                        "/usr/local/bin/git",
                        "/usr/bin/git"
                    };

                    // 存在するパスで最初に見つかったもの
                    return exePaths.FirstOrDefault(exePath => File.Exists(exePath));
                }

                // Windowsはこれだけで十分
                return "git";
            }

            /// <summary>
            /// コマンドを実行して標準出力を取得する。
            /// </summary>
            /// <param name="exePath">実行ファイルのパス</param>
            /// <param name="arguments">コマンドライン引数</param>
            /// <returns>標準出力</returns>
            static private string GetStandardOutputFromProcess(string exePath, string arguments)
            {
                // プロセスの起動条件を設定する。
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = exePath,
                    Arguments = arguments,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                };

                // プロセスを起動する。
                using (Process process = Process.Start(startInfo))
                {
                    // 標準出力を取得する。
                    string output = process.StandardOutput.ReadToEnd();

                    // プロセスが終了するかタイムアウトするまで待つ。
                    process.WaitForExit();

                    return output;
                }
            }
        };

        public class DxDiag
        {
            /// <summary>
            /// コマンドを実行して標準出力を取得する。
            /// </summary>
            /// <param name="exePath">実行ファイルのパス</param>
            /// <param name="arguments">コマンドライン引数</param>
            /// <returns>標準出力</returns>
            static public XDocument GetStandardOutputFromProcess()
            {
                // プロセスの起動条件を設定する。
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "dxdiag",
                    Arguments = "/x log.xml",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                };

                // プロセスを起動する。
                using (Process process = Process.Start(startInfo))
                {
                    // 標準出力を取得する。
                    string output = process.StandardOutput.ReadToEnd();

                    // プロセスが終了するかタイムアウトするまで待つ。
                    process.WaitForExit();

                    XDocument xml = XDocument.Load("log.xml");

                    return xml;
                }
            }
        };

        static public string GetUnityVersion()
        {
            return UnityEditorInternal.InternalEditorUtility.GetFullUnityVersion();
        }

        /*
         *TODO
        static public PCInfo GetPCInfo()
        {
            return GitUtility.GetLog();
        }
        */
    }

}