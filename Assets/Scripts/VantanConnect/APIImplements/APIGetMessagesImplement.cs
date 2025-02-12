using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{
    //GETなのでリクエストパラメータは関数で指定


    /// <summary>
    /// メッセージ
    /// </summary>
    [Serializable]
    public class APIUserMessages
    {
        public int Id;
        public int ToUserId;
        public int FromUserId;
        public string Message;
        public int Emotion;
        public string CreatedAt;
    }

    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GetMessagesResult : APIResponce
    {
        public int Count;
        public APIUserMessages[] Messages;
    }

    /// <summary>
    /// おうえんメッセージの取得
    /// </summary>
    public class APIGetMessagesImplement
    {
        /// <summary>
        /// メッセージの取得
        /// </summary>
        async public UniTask<GetMessagesResult> Request(int userId, int page = 0)
        {
            string request = String.Format("{0}/vc/messages/{1}?page={2}", VantanConnect.Environment.APIServerURI, userId, page);

            //かえってくるまでリトライ
            string json = null;
            for (int i = 0; i < 5; ++i)
            {
                json = await Network.WebRequest.GetRequest(request);
                if (json != null) break;

                await UniTask.Delay(500);
            }

            if (json == null) throw new Exception("サーバがダウンしている");

            var ret = JsonUtility.FromJson<GetMessagesResult>(json);
            return ret;
        }
    }
}