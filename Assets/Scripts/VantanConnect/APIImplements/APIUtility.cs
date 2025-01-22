
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
}