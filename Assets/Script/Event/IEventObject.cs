using UnityEngine.UI;
using UnityEngine;

public interface IEventObject
{
    //EventData _eventData { get; }
    Sprite _resultImage {get; set; }
    string _resultName { get; set; }
    string _resultText { get; set; }

    /// <summary>
    /// フラグの状態によって取り出すEventDataを判定する
    /// </summary>
    public void ResultFlagCheck();

}
