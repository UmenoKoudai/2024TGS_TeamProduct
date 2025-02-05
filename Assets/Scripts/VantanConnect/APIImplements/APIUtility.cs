
using UnityEngine;
using VTNConnect;

public class APIUtility
{
    static public VC_StatusCode PacketCheck(APIResponce result)
    {
        if (result == null)
        {
            Debug.LogError("戻り値がありませんでした。プログラムエラーかネットワークエラーです。");
            return VC_StatusCode.NetworkError;
        }
        if (result.Status != 200)
        {
            Debug.LogError($"エラーが返されました。サーバエラー:{result.Status}");
            return VC_StatusCode.SerrverError;
        }
        return VC_StatusCode.OK;
    }

    /// <summary>
    /// パラメータがほぼ同じデータの型変換を行う(非リフレクション)
    /// </summary>
    /// <typeparam name="T">変換後の型</typeparam>
    /// <typeparam name="V">元の型</typeparam>
    /// <param name="original">元の型の情報</param>
    /// <returns>変換後の情報</returns>
    static public T Marshal<T, V>(V original)
    {
        //一度Jsonにする
        string json = JsonUtility.ToJson(original);
        return JsonUtility.FromJson<T>(json);
    }
}