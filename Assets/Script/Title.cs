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
                _fadeSystem = FindObjectOfType<FadeSystem>();
                SceneManager.sceneLoaded += FadeIn;
                if(UnityEngine.Random.value < 0.01f)
                {
                    _fadeSystem.Play(FadeSystem.AnimType.Special);
                }
                else
                {
                    _fadeSystem.Play(FadeSystem.AnimType.FadeOut);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                SceneManager.LoadScene(sceneName);
            });
        }

        public void FadeIn(Scene scene, LoadSceneMode mode)
        {
            _fadeSystem.Play(FadeSystem.AnimType.FadeIn);
        }

        public void LoadPanelOpen()
        {
            FindObjectOfType<SaveLoadManager>().OpenLoadPanel();
        }
    }
}
