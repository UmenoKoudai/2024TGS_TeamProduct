using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using static VTNConnect.EventData;

namespace VTNConnect
{
    /// <summary>
    /// ゲーム物語クラス
    /// NOTE: この情報を集積して、ログができます
    /// </summary>
    [Serializable]
    public class GameEpisode
    {
        /// <summary>
        /// データパック
        /// </summary>
        [Serializable]
        public class ParamData
        {
            public string Description;
            public string Data;
        }

        [SerializeField] protected EpisodeCode EpisodeCode;                                 // エピソードコード(ルールシートを参照)
        [SerializeField] protected string Episode;                                  // 状況説明
        [SerializeField] protected int UserId = 0;                                  // ユーザID(半自動付与)
        [SerializeField] protected string GameHash = "";                            // ゲームハッシュ(自動付与)
        [SerializeField] protected List<ParamData> Payload = new List<ParamData>(); // 補足情報

        public GameEpisode(EpisodeCode epiCode, int userId)
        {
            EpisodeCode = epiCode;
            UserId = userId;
            GameHash = VantanConnect.GameHash; //あんまりきれいではないが
        }

        public GameEpisode(GameEpisode d)
        {
            EpisodeCode = d.EpisodeCode;
            UserId = d.UserId;
            Episode = d.Episode;
            Payload = d.Payload;
            GameHash = d.GameHash;
        }

        /// <summary>
        /// ただしく送信できるかどうかのチェック用
        /// </summary>
        public bool IsValidEpisode => UserId != 0 && GameHash != "";

        /// <summary>
        /// 状況を補足するテキストがあれば追加できます
        /// </summary>
        /// <param name="epi">状況を補足するテキスト</param>
        public void SetEpisode(string epi)
        {
            Episode = epi;
        }

        /// <summary>
        /// 補足データを格納します
        /// NOTE: ToString出来ない場合は正常に動作しない可能性があります。
        /// </summary>
        public void DataPack<T>(string desc, T data)
        {
            Payload.Add(new ParamData()
            {
                Description = desc,
                Data = data.ToString()
            });
        }
    }
}