using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VTNConnect;

namespace ToyBox
{
    public class Title : MonoBehaviour
    {
        private FadeSystem _fadeSystem;
        private void Start()
        {
            //タイトルに戻ってきたとき、過去の情報がリセットされる仕組み
            VantanConnect.SystemReset();
        }

        public void SceneChange(string sceneName)
        {
            VantanConnect.GameStart(async (VC_StatusCode code) =>
            {
                _fadeSystem.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                SceneManager.LoadScene(sceneName);
            });
        }
    }
}
