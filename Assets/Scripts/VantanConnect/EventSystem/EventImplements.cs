using System;

namespace VTNConnect
{
    /// <summary>
    /// 応援イベントクラス
    /// </summary>
    [Serializable]
    public class CheerEvent : EventData
    {
        public CheerEvent() : base(EventDefine.Cheer){ }
        public CheerEvent(EventData d) : base(d) { }


        /// <summary>
        /// メッセージを得る
        /// </summary>
        public string GetMessage()
        {
            return GetStringData("Message");
        }

        /// <summary>
        /// 感情値を得る
        /// NOTE: -100～100まで
        /// </summary>
        public int GetEmotion()
        {
            return GetIntData("Emotion");
        }
    }
}