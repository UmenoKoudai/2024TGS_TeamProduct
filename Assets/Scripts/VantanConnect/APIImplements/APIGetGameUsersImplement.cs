using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace VTNConnect
{

    /// リクエストパラメータはメソッドで指定する


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GetActiveGameUsersResult
    {
        public int Status;
        public UserData[] UserData;
    }


    /// <summary>
    /// 戻り値
    /// </summary>
    [Serializable]
    public class GetGameUsersResult : APIResponce
    {
        public UserData[] UserData;
    }



    /// <summary>
    /// AIゲーム情報取得
    /// NOTE: https://candle-stoplight-544.notion.site/API-def8a39d6b524c0fbf9e1a552d4b5428#17539cbfbab980e3863fe9aad40d2afc
    /// </summary>
    public class APIGetGameUsersImplement
    {
        /// <summary>
        /// 現在のゲーム参加情報取得
        /// </summary>
        /// <returns>ユーザ情報</returns>
        async public UniTask<GetActiveGameUsersResult> Request()
        {
            string request = String.Format("{0}/vc/gameusers/active", VantanConnect.Environment.APIServerURI);
            string json = await Network.WebRequest.GetRequest(request);
            var ret = JsonUtility.FromJson<GetActiveGameUsersResult>(json);
            return ret;
        }

        /// <summary>
        /// 特定のゲーム情報取得(GameHashを指定する)
        /// </summary>
        /// <returns>ユーザ情報</returns>
        async public UniTask<GetGameUsersResult> Request(string gameHash)
        {
            string request = String.Format("{0}/vc/gameusers/{1}", VantanConnect.Environment.APIServerURI, gameHash);
            string json = await Network.WebRequest.GetRequest(request);
            var ret = JsonUtility.FromJson<GetGameUsersResult>(json);
            return ret;
        }
    }
}