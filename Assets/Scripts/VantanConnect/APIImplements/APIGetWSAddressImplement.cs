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
            string request = String.Format("{0}/getaddr", VantanConnect.Environment.APIServerURI);
            string json = await Network.WebRequest.GetRequest(request);
            var ret = JsonUtility.FromJson<GetAddrResult>(json);
            return ret.Address;
        }
    }
}