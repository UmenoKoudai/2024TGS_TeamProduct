using UnityEngine.UI;
using UnityEngine;

public interface IEventObject
{
    EventData EventData { get; }
    Sprite ResultImage {get; set; }
    string ResultName { get; set; }
    string ResultText { get; set; }

    /// <summary>
    /// フラグの状態によって取り出すEventDataを判定する
    /// </summary>
    public void ResultFlagCheck();

}
