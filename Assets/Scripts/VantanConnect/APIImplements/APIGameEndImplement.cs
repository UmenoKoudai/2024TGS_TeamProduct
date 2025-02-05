using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// リクエストパラメータ
    /// </summary>
    [Serializable]
    public class GameEndRequest
    {
        public string GameHash;
        public bool GameResult;
    }


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GameEndResult : APIResponce
    {
    }

    /// <summary>
    /// ゲーム終了
    /// NOTE: https://candle-stoplight-544.notion.site/API-def8a39d6b524c0fbf9e1a552d4b5428#17539cbfbab980e3863fe9aad40d2afc
    /// </summary>
    public class APIGameEndImplement
    {
        /// <summary>
        /// 通常ゲーム用
        /// </summary>
        /// <returns>特になし</returns>
        async public UniTask<GameEndResult> Request(GameEndRequest req)
        {
            string request = String.Format("{0}/gameend", VantanConnect.Environment.APIServerURI);

            //かえってくるまでリトライ
            string json = null;
            for (int i = 0; i < 5; ++i)
            {
                json = await Network.WebRequest.PostRequest(request, req);
                if (json != null) break;

                await UniTask.Delay(500);
            }

            if (json == null) throw new Exception("サーバがダウンしている");

            var ret = JsonUtility.FromJson<GameEndResult>(json);
            return ret;
        }
    }
}