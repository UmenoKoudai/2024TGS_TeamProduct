using Cysharp.Threading.Tasks;
using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace VTNConnect
{
    /// <summary>
    /// OpenAIのRealtime-API
    /// </summary>
    public class RealtimeOpenAI : MonoBehaviour
    {
        const string ModelName = "gpt-4o-mini-realtime-preview";
        const string BaseURI = "wss://api.openai.com/v1/realtime";

        public delegate void ResponceCallback(string message);

        //クライアント実装
        WebSocketCli _client = new WebSocketCli();
        ResponceCallback _callback = null;

        [Serializable]
        class OpenAIResponse
        {
            public string[] modalities = new string[] { "text" };
            public string instructions = "";
        };

        [Serializable]
        class OpenAITextContent
        {
            public string type = "message";
            public string text = "user";
        };

        [Serializable]
        class OpenAITextItem
        {
            public string type = "message";
            public string role = "user";
            public OpenAITextContent[] content = new OpenAITextContent[1];
        };

        [Serializable]
        class TextEvent
        {
            public string event_id = Guid.NewGuid().ToString();
            public string type = "conversation.item.create";
            public OpenAITextItem item = new OpenAITextItem();
        }

        [Serializable]
        class RequestResponseEvent
        {
            public string type = "response.create";
            public OpenAIResponse response = new OpenAIResponse();
        }

        void OnDestroy()
        {
            _client?.Close();
        }

        void Update()
        {
            _client.Update();
        }

        async public void Connect(ResponceCallback callback)
        {
            _callback = callback;

            var api = new APIGetRealtimeAIKeyImplement();
            var key = await api.Request("", "冒険の書を作る人");

            Dictionary<string, string> header = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + VantanConnect.SystemSave.OpenAIAPIKey },
                { "OpenAI-Beta", "realtime=v1" },
                { "Content-Type", "application/json" }
            };
            await _client.Connect(string.Format("{0}?model={1}", BaseURI, ModelName), Message, header);
        }

        public void SendText(string msg)
        {
            if (_client.IsClosed) return;

            TextEvent evt = new TextEvent();
            evt.item.content[0] = new OpenAITextContent() { type = "input_text", text = msg };
            string json = JsonUtility.ToJson(evt);
            _client.Send(json);
            Debug.Log(json);
        }

        public void RequestResponse()
        {
            if (_client.IsClosed) return;

            RequestResponseEvent req = new RequestResponseEvent();
            string req_json = JsonUtility.ToJson(req);
            _client.Send(req_json);
            Debug.Log(req_json);
        }

        void Message(byte[] msg)
        {
            WebSocketPacket data = null;
            try
            {
                string json = Encoding.UTF8.GetString(msg);
                Debug.Log(json);
                _callback?.Invoke(json);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}