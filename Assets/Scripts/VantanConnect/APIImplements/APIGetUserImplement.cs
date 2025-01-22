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
    public class GetUserResult : APIResponce
    {
        public UserData UserData;
    }


    /// <summary>
    /// ゲーム終了
    /// NOTE: https://candle-stoplight-544.notion.site/API-def8a39d6b524c0fbf9e1a552d4b5428#16a39cbfbab980d1b861f696d0f99f8e
    /// </summary>
    public class APIGetUserImplement
    {
        /// <summary>
        /// ユーザデータ取得(UserIdから取得)
        /// NOTE: withLogは指定しなくても良い
        /// </summary>
        /// <returns>ユーザ情報</returns>
        async public UniTask<GetUserResult> Request(int userId, bool withLog = false, int logCount = 0)
        {
            string request = String.Format("{0}/user/{1}", VantanConnect.Environment.APIServerURI, userId);
            if (withLog)
            {
                request += "?withLog=1&logCount" + logCount;
            }

            string json = await Network.WebRequest.GetRequest(request);
            var ret = JsonUtility.FromJson<GetUserResult>(json);
            return ret;
        }

        /// <summary>
        /// ユーザデータ取得(UserHashから取得)
        /// NOTE: withLogは指定しなくても良い
        /// </summary>
        /// <returns>ユーザ情報</returns>
        async public UniTask<GetUserResult> Request(string userHash, bool withLog, int logCount)
        {
            string request = String.Format("{0}/user/{1}", VantanConnect.Environment.APIServerURI, userHash);
            if (withLog)
            {
                request += "?withLog=1&logCount" + logCount;
            }

            string json = await Network.WebRequest.GetRequest(request);
            var ret = JsonUtility.FromJson<GetUserResult>(json);
            return ret;
        }
    }
}