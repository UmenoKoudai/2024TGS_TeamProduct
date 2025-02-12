using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// リクエストパラメータ
    /// </summary>
    [Serializable]
    public class GameStartRequest
    {
        public int GameId;
        public int UserId = 0;
        public int Option = 0;
    }

    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GameStartResult : APIResponce
    {
        public string GameHash;
        public GameInfo[] GameInfo;
    }

    /// <summary>
    /// ゲーム開始
    /// NOTE: https://candle-stoplight-544.notion.site/API-def8a39d6b524c0fbf9e1a552d4b5428#17539cbfbab980afa7acc523767846a3
    /// </summary>
    public class APIGameStartImplement
    {
        /// <summary>
        /// 通常ゲーム用
        /// </summary>
        /// <returns>特になし</returns>
        async public UniTask<GameStartResult> Request(GameStartRequest req)
        {
            string request = String.Format("{0}/vc/gamestart", VantanConnect.Environment.APIServerURI);
            string json = await Network.WebRequest.PostRequest(request, req);
            var ret = JsonUtility.FromJson<GameStartResult>(json);
            return ret;
        }
    }
}