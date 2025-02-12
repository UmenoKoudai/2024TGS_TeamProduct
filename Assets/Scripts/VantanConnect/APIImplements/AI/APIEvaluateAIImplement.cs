using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{

    /// <summary>
    /// リクエスト
    /// </summary>
    [Serializable]
    public class APIEvaluateAIRequest : APIResponce
    {
        public string Prompt;
    }

    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class APIEvaluateAIResult : APIResponce
    {
        public string Result;
    }

    /// <summary>
    /// AIの試験用
    /// </summary>
    public class APIEvaluateAIImplement
    {
        public enum AIType
        {
            OpenAI = 1,
            Anthropic = 2,
            Google = 3
        }

        async public UniTask<APIEvaluateAIResult> Request(string prompt)
        {
            APIEvaluateAIRequest req = new APIEvaluateAIRequest();
            string request = String.Format("{0}/ai/all/eval", VantanConnect.Environment.APIServerURI);

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

            var ret = JsonUtility.FromJson<APIEvaluateAIResult>(json);
            return ret;
        }
    }
}