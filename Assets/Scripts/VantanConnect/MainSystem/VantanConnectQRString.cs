using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// バンコネ用のQR文字列を生成する
    /// </summary>
    public class VantanConnectQRString
    {
        class GameLink
        {
            public int GameID = ProjectSettings.GameID;
            public int EventId = (int)LinkageSyatem.VC_LinkageEvent.Link;
        };

        /// <summary>QRコード用の文字列を生成する</summary>
        static public string MakeQRStringLinkage()
        {
            var json = new GameLink();
            return string.Format("VantanConnectQR:{0}", JsonUtility.ToJson(json));
        }
    }
}