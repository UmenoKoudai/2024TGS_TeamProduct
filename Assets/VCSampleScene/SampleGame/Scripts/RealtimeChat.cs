using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// リアルタイムチャット
    /// </summary>
    public class RealtimeChat : MonoBehaviour
    {
        [SerializeField] InputField _input;
        [SerializeField] Text _text;
        [SerializeField] RealtimeOpenAI _openAI;

        public void Start()
        {
            Setup();
        }

        async void Setup()
        {
            _openAI.Connect(Message);
        }

        public void SendChat()
        {
            _openAI.SendText(_input.text);
            _input.text = "";
        }

        public void SendRequest()
        {
            _openAI.RequestResponse();
        }

        void Message(string msg)
        {
            _text.text += "\n---------------------------------------------------\n" + msg;
        }
    }
}