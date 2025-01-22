using System.Collections.Generic;
using UnityEngine;
using MessagePack;
using System;
using System.Text;

namespace VTNConnect
{
    /// <summary>
    /// WebSocketのイベント処理や監視をするクラス
    /// </summary>
    public class WebSocketEventManager : MonoBehaviour
    {
        //インスペクタ見る用
        [SerializeField] bool _isConnected;
        [SerializeField] int _gameId;

        public bool IsConnecting { get; private set; } = false;

        //アドレス取得API
        APIGetWSAddressImplement _getAddress = new APIGetWSAddressImplement();

        //クライアント実装
        WebSocketCli _client = new WebSocketCli();

        //イベント管理
        Queue<EventData> _sendQueue = new Queue<EventData>();
        Queue<EventData> _eventQueue = new Queue<EventData>();

        //コールバック
        EventSystem.EventDataCallback _event = null;

        //接続したさいの識別ID
        string _sessionId = null;

#if UNITY_EDITOR
        [SerializeField] List<EventData> _sendEventLog = new List<EventData>();
        [SerializeField] List<EventData> _recvEventLog = new List<EventData>();
#endif


        private void Start()
        {
            Setup();
        }

        async public void Setup()
        {
            if (IsConnecting) return;

            _gameId = ProjectSettings.GameID;
            string address = await _getAddress.Request();
            Connect(address);
        }

        public void SetEventSystem(EventSystem system)
        {
            system.Setup(Send, out _event);
        }

        void Update()
        {
            if (_eventQueue.Count > 0)
            {
                foreach (var msg in _eventQueue)
                {
                    _event.Invoke(msg);
#if UNITY_EDITOR
                    _recvEventLog.Add(msg);
#endif
                }
                _eventQueue.Clear();
            }

            if (_sendQueue.Count == 0) return;

            var d = _sendQueue.Dequeue();

            WSPS_SendEvent data = new WSPS_SendEvent(_sessionId, d);
            _client.Send(JsonUtility.ToJson(data));
#if UNITY_EDITOR
            _sendEventLog.Add(data);
#endif
        }


        void Connect(string address)
        {
            _client.Connect(address, Message);
        }

        void Send(EventData data)
        {
            _sendQueue.Enqueue(data);
        }

        void Message(byte[] msg)
        {
            WebSocketPacket data = null;
            try
            {
                //data = MessagePackSerializer.Deserialize<ServerResult>(msg);
                string json = Encoding.UTF8.GetString(msg);
                data = JsonUtility.FromJson<WebSocketPacket>(json);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            if (data == null) return;

            try
            {
                switch ((WebSocketCommand)data.Command)
                {
                    case WebSocketCommand.WELCOME:
                        {
                            var welcome = JsonUtility.FromJson<WSPR_Welcome>(data.Data);
                            var join = new WSPS_Join();
                            join.SessionId = welcome.SessionId;
                            join.GameId = _gameId;
                            _client.Send(JsonUtility.ToJson(join));
                            _sessionId = welcome.SessionId;
                        }
                        break;

                    case WebSocketCommand.EVENT:
                        {
                            var evt = JsonUtility.FromJson<EventData>(data.Data);
                            _eventQueue.Enqueue(evt);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}