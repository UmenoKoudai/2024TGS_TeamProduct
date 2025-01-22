using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using static Network.WebRequest;
using Cysharp.Threading.Tasks;


namespace DataManagement
{
    /// <summary>
    /// マスターデータ管理クラス
    /// NOTE: このクラスは破壊的変更を行う可能性があるので注意
    /// </summary>
    public partial class MasterData
    {
        //設定
        const string DataPrefix = "DataAsset/MasterData";


        //マスターデータ読み込みリスト
        static public TextMaster TextMaster { get; private set; }


        //読み込み処理
        async UniTask MasterDataLoad()
        {
            //マスタ読み込み
            TextMaster = new TextMaster();

            await UniTask.WhenAll(new List<UniTask>()
            {
                TextMaster.Marshal(),
            });
        }
    }
}