using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{

    /// <summary>
    /// リクエスト
    /// </summary>
    [Serializable]
    public class GetRealtimeAIKeyRequest : APIResponce
    {
        public string ModelName;
        public string Instructions;
    }

    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GetRealtimeAIKeyResult : APIResponce
    {
        public string SessionId;
        public string EphemeralKey;
        public long KeyExpiresAt;
    }

    /// <summary>
    /// RealtimeAI用のキーの取得取得
    /// </summary>
    public class APIGetRealtimeAIKeyImplement
    {
        async public UniTask<GetRealtimeAIKeyResult> Request(string model, string instructions)
        {
            GetRealtimeAIKeyRequest req = new GetRealtimeAIKeyRequest();
            string request = String.Format("{0}/tools/realtimeai", VantanConnect.Environment.APIServerURI);

            req.ModelName = model;
            req.Instructions = instructions;

            //かえってくるまでリトライ
            string json = null;
            for (int i = 0; i < 5; ++i)
            {
                json = await Network.WebRequest.PostRequest(request, req);
                if (json != null) break;

                await UniTask.Delay(500);
            }

            if (json == null) throw new Exception("サーバがダウンしている");

            var ret = JsonUtility.FromJson<GetRealtimeAIKeyResult>(json);
            return ret;
        }
    }
}