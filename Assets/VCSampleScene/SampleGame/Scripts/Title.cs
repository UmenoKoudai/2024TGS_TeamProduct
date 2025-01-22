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
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //ゲームスタート時に通信する
                VantanConnect.GameStart((VC_StatusCode code) =>
                {
                    SceneManager.LoadScene("InGame");
                });
            }
        }
    }
}