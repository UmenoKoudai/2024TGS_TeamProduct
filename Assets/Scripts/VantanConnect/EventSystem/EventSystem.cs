using System.Collections.Generic;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// イベントリレー管理クラス
    /// </summary>
    public class EventSystem
    {
        public delegate void EventDataSender(EventData data);
        public delegate void EventDataCallback(EventData data);
        EventDataSender _sender = null;
        List<IVantanConnectEventReceiver> _initialSavedListener = new List<IVantanConnectEventReceiver>();
        List<IVantanConnectEventReceiver> _eventListener = new List<IVantanConnectEventReceiver>();
        List<IVantanConnectEventReceiver> _reNewListener = new List<IVantanConnectEventReceiver>();

        /// <summary>
        /// データリレー関数
        /// NOTE: APIやWebSocketからコールされる
        /// </summary>
        private void DataReceive(EventData data)
        {
            //nullが無いように調整する
            bool isRenew = false;
            foreach (var ev in _eventListener)
            {
                if (ev == null)
                {
                    isRenew = true;
                    _reNewListener.Clear();
                }
            }

            foreach (var ev in _eventListener)
            {
                if (ev == null)
                {
                    continue;
                }

                if (isRenew)
                {
                    _reNewListener.Add(ev);
                }

                if (ev.IsActive == false)
                {
                    continue;
                }

                ev.OnEventCall(data);
            }

            if(isRenew)
            {
                _eventListener = _reNewListener;
            }
        }

        /// <summary>
        /// 現在の状態をセーブする
        /// </summary>
        public void SystemInitialSave()
        {
            _initialSavedListener.Clear();
            foreach (var ev in _eventListener)
            {
                _initialSavedListener.Add(ev);
            }
        }

        /// <summary>
        /// システムの状態を初期化する
        /// </summary>
        public void Reset()
        {
            //最初に購読していたシステムのリスナーだけにする
            _eventListener.Clear();
            foreach (var ev in _initialSavedListener)
            {
                _eventListener.Add(ev);
            }
        }

        /// <summary>
        /// メインシステムから呼び出される
        /// </summary>
        public void Setup(EventDataSender send, out EventDataCallback recv)
        {
            _sender = send;
            recv = DataReceive;
        }


        /// <summary>
        /// 購読開始
        /// </summary>
        public void RegisterReceiver(IVantanConnectEventReceiver receiver)
        {
            _eventListener.Add(receiver);
        }

        /// <summary>
        /// イベントを送信
        /// </summary>
        public void SendEvent(EventData data)
        {
            _sender?.Invoke(data);
        }

#if UNITY_EDITOR
        /// <summary>
        /// テスト用のイベント実行関数
        /// </summary>
        public void RunEvent(EventData data)
        {
            DataReceive(data);
        }
#endif
    }
}