using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// タイトル
    /// </summary>
    public class Title : MonoBehaviour
    {
        private void Start()
        {
            //タイトルに戻ってきたとき、過去の情報がリセットされる仕組み
            VantanConnect.SystemReset();

            if (VantanConnect.SystemSave.IsDebugConnect)
            {
                TestMessageRecv();
            }
        }

        async void TestMessageRecv()
        {
            //メッセージ
            APIGetMessagesImplement api = new APIGetMessagesImplement();
            var messages = await api.Request(VantanConnect.SystemSave.UseConnectUserId);
            Debug.Log(messages.Messages);
        }

        private void Update()
        {
#if AIGAME_IMPLEMENT
            //ゲームスタート時に通信する
            VantanConnect.GameStart().Forget();
            SceneManager.LoadScene("InGame");
#else
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //ゲームスタート時に通信する
                VantanConnect.GameStart((VC_StatusCode code) =>
                {
                    SceneManager.LoadScene("InGame");
                });
            }
#endif
        }
    }
}