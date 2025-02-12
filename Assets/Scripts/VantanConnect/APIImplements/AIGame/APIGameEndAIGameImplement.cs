using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

#if AIGAME_IMPLEMENT
namespace VTNConnect
{
    /// <summary>
    /// リクエストパラメータ
    /// </summary>
    [Serializable]
    public class GameEndAIGameRequest
    {
        public string GameHash;
        public UserDataResultSave[] UserResults;
    }


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GameEndAIGameResult : APIResponce
    {
        public GameInfo[] GameInfo;
    }


    /// <summary>
    /// ゲーム終了(AIゲーム用)
    /// NOTE: https://candle-stoplight-544.notion.site/API-def8a39d6b524c0fbf9e1a552d4b5428#16a39cbfbab9809c83d5efe15acd0e52
    /// </summary>
    public class APIGameEndAIGameImplement
    {
        /// <summary>
        /// AIゲーム用
        /// </summary>
        /// <returns>AIゲーム開始用パラメータ</returns>
        async public UniTask<GameEndAIGameResult> Request(GameEndAIGameRequest req)
        {
            string request = String.Format("{0}/vc/ai/gameend", VantanConnect.Environment.APIServerURI);
            string json = await Network.WebRequest.PostRequest(request, req);
            var ret = JsonUtility.FromJson<GameEndAIGameResult>(json);
            return ret;
        }
    }
}
#endif