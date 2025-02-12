using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{

    //このAPIはリクエストパラメータはなし


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GetAddrResult : APIResponce
    {
        public string Address;
    }

    /// <summary>
    /// WebSocketアドレス取得
    /// </summary>
    public class APIGetWSAddressImplement
    {
        async public UniTask<string> Request()
        {
            string request = String.Format("{0}/vc/getaddr", VantanConnect.Environment.APIServerURI);

            //かえってくるまでリトライ
            string json = null;
            for (int i = 0; i < 5; ++i)
            {
                json = await Network.WebRequest.GetRequest(request);
                if (json != null) break;

                await UniTask.Delay(500);
            }

            if (json == null) throw new Exception("サーバがダウンしている");

            var ret = JsonUtility.FromJson<GetAddrResult>(json);
            return ret.Address;
        }
    }
}