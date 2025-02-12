using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

#if AIGAME_IMPLEMENT
namespace VTNConnect
{

    /// リクエストパラメータなし


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GameStartAIGameResult : APIResponce
    {
        public string GameHash;
        public string GameTitle;
        public UserData[] GameUsers;
        public GameInfo[] GameInfo;
    }

    /// <summary>
    /// ゲーム終了(AIゲーム用)
    /// NOTE: https://candle-stoplight-544.notion.site/API-def8a39d6b524c0fbf9e1a552d4b5428#16a39cbfbab9809c83d5efe15acd0e52
    /// </summary>
    public class APIGameStartAIGameImplement
    {
        /// <summary>
        /// ゲーム終了(AIゲーム用)
        /// </summary>
        /// <param name="instance">APIインスタンス</param>
        /// <returns>リザルト</returns>
        async public UniTask<GameStartAIGameResult> Request()
        {
            string request = String.Format("{0}/vc/ai/gamestart", VantanConnect.Environment.APIServerURI);
            string json = await Network.WebRequest.PostRequest(request, "{}");
            var ret = JsonUtility.FromJson<GameStartAIGameResult>(json);
            return ret;
        }
    }
}
#endif