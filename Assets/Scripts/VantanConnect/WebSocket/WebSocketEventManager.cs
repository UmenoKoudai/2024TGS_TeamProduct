using System.Collections.Generic;
using UnityEngine;
using MessagePack;
using System;
using System.Text;
using static VTNConnect.EventSystem;
using Cysharp.Threading.Tasks;

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

        //再接続
        bool _isReconnect = true;

        //アドレス取得API
        APIGetWSAddressImplement _getAddress = new APIGetWSAddressImplement();

        //クライアント実装
        WebSocketCli _client = new WebSocketCli();

        //イベント管理
        Queue<string> _sendQueue = new Queue<string>();
        Queue<EventData> _eventQueue = new Queue<EventData>();

        //コールバック
        EventSystem.EventDataCallback _event = null;

        //接続したさいの識別ID
        string _sessionId = null;

#if UNITY_EDITOR
        List<string> _sendEventLog = new List<string>();
        List<EventData> _recvEventLog = new List<EventData>();
#endif


        private void Start()
        {
            Setup();
        }

        void OnDestroy()
        {
            _client?.Close();
        }

        async public void Setup()
        {
            _gameId = ProjectSettings.GameID;
            string address = await _getAddress.Request();
            _isReconnect = false;
            if (address == "") return;
            Connect(address);
        }

        public void Setup(EventDataCallback callback)
        {
            _event = callback;
        }

        void Update()
        {
            //切断されていたら再接続
            if (_client.IsClosed)
            {
                if (_isReconnect == false)
                {
                    _isReconnect = true;
                    UniTask.Delay(2500).ContinueWith(() => { Debug.Log("ReConnect");  Setup(); }).Forget();
                }
                return;
            }

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
            _client.Send(d);
#if UNITY_EDITOR
            _sendEventLog.Add(d);
#endif
        }


        void Connect(string address)
        {
            _client.Connect(address, Message);
        }

        public void Send(EventData data)
        {
            WSPS_SendEvent pack = new WSPS_SendEvent(_sessionId, data);
            _sendQueue.Enqueue(JsonUtility.ToJson(pack));
        }

        public void Send(GameEpisode data)
        {
            WSPS_SendEpisode pack = new WSPS_SendEpisode(_sessionId, data);
            _sendQueue.Enqueue(JsonUtility.ToJson(pack));
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

                    case WebSocketCommand.GAMESTAT:
                        {
#if UNITY_EDITOR
                            var stat = JsonUtility.FromJson<WSPR_GameStat>(data.Data);
                            EditorDataRelay.UpdateGameStat(stat);
#endif
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