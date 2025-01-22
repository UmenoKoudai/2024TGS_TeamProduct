using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// イベントレシーバーインタフェース
    /// </summary>
    public interface IVantanConnectEventReceiver
    {
        //アクティブなレシーバーかどうか
        public bool IsActive { get; }

        //イベント受信関数
        void OnEventCall(EventData data);
    }
}