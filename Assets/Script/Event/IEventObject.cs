using UnityEngine.UI;

public interface IEventObject
{
    EventData _eventData { get; }
    Image _resultImage {get; set; }
    string _resultName { get; set; }
    string _resultText { get; set; }

    /// <summary>
    /// フラグの状態によって取り出すEventDataを判定する
    /// </summary>
    public void ResultFlagCheck();

}
