using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// リクエストパラメータ
    /// </summary>
    [Serializable]
    public class GameHandOverRequest
    {
        public int GameId;
        public int UserId;
        public int Option;
    }


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GameHandOverResult : APIResponce
    {
        public string GameHash;
        public UserData UserData;
    }

    /// <summary>
    /// ゲーム引継ぎ
    /// NOTE: 
    /// </summary>
    public class APIGameHandOverImplement
    {
        /// <summary>
        /// 通常ゲーム用
        /// </summary>
        async public UniTask<GameHandOverResult> Request(GameHandOverRequest req)
        {
            string request = String.Format("{0}/vc/handover", VantanConnect.Environment.APIServerURI);

            //かえってくるまでリトライ
            string json = null;
            for (int i = 0; i < 5; ++i)
            {
                json = await Network.WebRequest.PostRequest(request, req);
                if (json != null) break;

                await UniTask.Delay(500);
            }

            if (json == null) throw new Exception("サーバがダウンしている");

            var ret = JsonUtility.FromJson<GameHandOverResult>(json);
            return ret;
        }
    }
}