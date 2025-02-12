using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// AI評価
    /// </summary>
    public class AIEvaluate : MonoBehaviour
    {
        [SerializeField] InputField _input;
        [SerializeField] InputField _inputOpenAI;
        [SerializeField] InputField _inputAnthropic;
        [SerializeField] InputField _inputGoogle;
        [SerializeField] Text _text;

        async public void SendOpenAI()
        {
            APISelectiveAIAccessImplement api = new APISelectiveAIAccessImplement();
            var result = await api.Request(APISelectiveAIAccessImplement.AIType.OpenAI, _inputOpenAI.text.Trim(), _input.text);
            _text.text = result.Result;
        }

        async public void SendAnthropic()
        {
            APISelectiveAIAccessImplement api = new APISelectiveAIAccessImplement();
            var result = await api.Request(APISelectiveAIAccessImplement.AIType.Anthropic, _inputAnthropic.text.Trim(), _input.text);
            _text.text = result.Result;
        }

        async public void SendGoogle()
        {
            APISelectiveAIAccessImplement api = new APISelectiveAIAccessImplement();
            var result = await api.Request(APISelectiveAIAccessImplement.AIType.Google, _inputGoogle.text.Trim(), _input.text);
            _text.text = result.Result;
        }

        async public void SendEvaluate()
        {
            APIEvaluateAIImplement api = new APIEvaluateAIImplement();
            var result = await api.Request(_input.text);
            _text.text = result.Result;
        }
    }
}