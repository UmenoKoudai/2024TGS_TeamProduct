using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VTNConnect
{
    /// <summary>
    /// イベントデータクラス
    /// NOTE: バンコネのデータはすべてこのクラスを派生してやり取りされます
    /// </summary>
    [Serializable]
    public class EventData
    {
        public EventDefine EventCode => (EventDefine)EventId;

        /// <summary>
        /// データを格納します
        /// NOTE: ToString出来ない場合は正常に動作しない可能性があります。
        /// </summary>
        public void DataPack<T>(string Key, T data)
        {
            Payload.Add(new ParamData()
            {
                Key = Key,
                TypeName = typeof(T).Name,
                Data = data.ToString()
            });
        }

        /// <summary>
        /// データをJsonでシリアライズして格納します
        /// NOTE: シリアライズ出来ない場合は正常に動作しない可能性があります。
        /// </summary>
        public void DataJsonSerializePack<T>(string Key, T data)
        {
            //シリアライズ可能かどうかチェック
            if(!typeof(T).IsSerializable)
            {
                Debug.LogError("シリアライズ可能な値ではありませんでした。代わりにDataPackを試みます");
                DataPack(Key, data);
                return;
            }

            Payload.Add(new ParamData()
            {
                Key = Key,
                TypeName = typeof(T).Name,
                Data = data.ToString()
            });
        }

        /// <summary>
        /// データを返します
        /// NOTE: もし構造体などを展開する場合は、TypeNameからデータ内容を推察してください。
        /// </summary>
        public ParamData GetData(string Key)
        {
            var target = Payload.Where(p => p.Key == Key);
            if (target.Count() == 0)
            {
                Debug.LogError($"データがNULLです:" + Key);
                return null;
            }
            return target.First();
        }

        /// <summary>
        /// 数字データを返します
        /// NOTE: 数字じゃないデータは0またはNaNが帰ります
        /// </summary>
        public int GetIntData(string Key)
        {
            var data = GetData(Key);
            if (data == null) return 0;
            if (data.TypeName != "Integer" && !data.TypeName.StartsWith("Int") && data.TypeName != "number")
            {
                Debug.LogWarning($"Intじゃない値かもしれません:{data.Data}({data.TypeName })");
            }
            return int.Parse(data.Data);
        }

        /// <summary>
        /// 文字列データを返します
        /// </summary>
        public string GetStringData(string Key)
        {
            var data = GetData(Key);
            if (data == null) return "";
            return data.Data;
        }


        /// <summary>
        /// データパック
        /// </summary>
        [Serializable]
        public class ParamData
        {
            public string Key;
            public string TypeName;
            public string Data;
        }

        #region 内部実装
        //シリアライズされるメンバ
        [SerializeField] public EventDefine EventId;                                    // イベントコード(ルールシートを参照)
        [SerializeField] protected int FromId = VantanConnect.GameID;                 // 誰から送信されたイベントか(自動付与)
        [SerializeField] protected string GameHash = "";                                // ゲームハッシュ(自動付与)
        [SerializeField] protected List<ParamData> Payload = new List<ParamData>();     // 補足情報
        //ここまで

        //主にコピーやイベントの変換に使う
        protected EventData(EventData d)
        {
            EventId = d.EventId;
            FromId = d.FromId;
            Payload = d.Payload;
            GameHash = d.GameHash;
        }

        //新しいイベントの作成に使う
        public EventData(EventDefine eventId)
        {
            EventId = eventId;
            GameHash = VantanConnect.GameHash; //あんまりきれいではないが
        }
        #endregion
    }
}