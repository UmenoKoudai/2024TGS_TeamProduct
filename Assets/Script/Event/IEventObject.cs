using UnityEngine.UI;

public interface IEventObject
{
    /// <summary> オーバーライドするプロパティ </summary>
    EventData EventData { get; }
    Image ResultImage { get; set; }
    string ResultName { get; set; }
    string ResultText { get; set; }

    /// <summary>
    /// フラグの状態によって取り出すEventDataを判定する
    /// </summary>
    public void ResultFlagCheck();

}
