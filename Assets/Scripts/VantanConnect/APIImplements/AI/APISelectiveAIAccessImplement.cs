using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{

    /// <summary>
    /// リクエスト
    /// </summary>
    [Serializable]
    public class APISelectiveAIAccessRequest : APIResponce
    {
        public string Model;
        public string Prompt;
    }

    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class APISelectiveAIAccessResult : APIResponce
    {
        public string Result;
    }

    /// <summary>
    /// AIの試験用
    /// </summary>
    public class APISelectiveAIAccessImplement
    {
        public enum AIType
        {
            OpenAI = 1,
            Anthropic = 2,
            Google = 3
        }

        async public UniTask<APISelectiveAIAccessResult> Request(AIType aitype, string model, string prompt)
        {
            APISelectiveAIAccessRequest req = new APISelectiveAIAccessRequest();
            string type = "";
            switch(aitype)
            {
                case AIType.OpenAI: type = "openai"; break;
                case AIType.Anthropic: type = "anthropic"; break;
                case AIType.Google: type = "google"; break;
            }
            string request = String.Format("{0}/ai/{1}/chat", VantanConnect.Environment.APIServerURI, type);

            req.Model = model;
            req.Prompt = prompt;

            //かえってくるまでリトライ
            string json = null;
            for (int i = 0; i < 5; ++i)
            {
                json = await Network.WebRequest.PostRequest(request, req);
                if (json != null) break;

                await UniTask.Delay(500);
            }

            if (json == null) throw new Exception("サーバがダウンしている");

            var ret = JsonUtility.FromJson<APISelectiveAIAccessResult>(json);
            return ret;
        }
    }
}