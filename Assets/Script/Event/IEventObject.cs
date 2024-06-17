using UnityEngine.UI;
using UnityEngine;

public interface IEventObject
{
    EventData EventData { get; }
    EventTalkData ResultEventTalkData { get; set; }

    /// <summary>
    /// フラグの状態によって取り出すEventDataを判定する
    /// </summary>
    public void ResultFlagCheck();

}
