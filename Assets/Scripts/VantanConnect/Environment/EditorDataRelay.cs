#if UNITY_EDITOR
namespace VTNConnect
{
    /// <summary>
    /// エディタでいろいろ共有する用のデータ保存クラス
    /// NOTE: エディタのみ使用
    /// </summary>
    public class EditorDataRelay
    {
        static EditorDataRelay _instance = new EditorDataRelay();
        EditorDataRelay() { }

        WSPR_GameStat _stat;

        static public WSPR_GameStat GameStat => _instance._stat;

        static public void UpdateGameStat(WSPR_GameStat stat)
        {
            _instance._stat = stat;
        }
    }
}
#endif