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
            //�^�C�g���ɖ߂��Ă����Ƃ��A�ߋ��̏�񂪃��Z�b�g�����d�g��
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
