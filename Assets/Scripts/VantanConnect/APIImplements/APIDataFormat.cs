using System;

namespace VTNConnect
{
    /// <summary>
    /// ゲーム内で使用する「アーティファクト」の情報が渡される
    /// </summary>
    [Serializable]
    public class ArtifactInfo
    {
        public int Id;
        public int OwnerId;
    }

    /// <summary>
    /// ユーザ情報
    /// </summary>
    [Serializable]
    public class UserData
    {
        //Idは無視する

        public int UserId;          //ユーザID。このIDをリクエスト時に送信する
        public string UserHash;     //ユーザハッシュ。何らかのアクセスをする際、特定されない用途に使用するUUIDv4.
        public int Type;            //ユニークユーザか一般ユーザか
        public string Name;         //名前。フルネーム
        public int Level;           //レベル。1～
        public int Exp;             //経験値。0～
        public int Karma;           //罪の値。0～
        public int Gold;            //所持ゴールド。アウトゲームのみ参照する想定。
        public int PlayCount;       //プレイ回数
        public DateTime CreatedAt;      //生成日時
        public DateTime LastPlayedAt;   //最後に遊んだ日
        public string DisplayName;  //表示名(短縮したフルネーム)
        public int AvatarType;      //見た目(アバターID)
        public string Gender;       //性別
        public int Age;             //年齢
        public string Job;          //職業
        public string Personality;  //個性
        public string Motivation;   //冒険のモチベ
        public string Weaknesses;   //弱点
        public string Background;   //バックストーリー
    };


    /// <summary>
    /// AIゲームリザルト用
    /// </summary>
    [Serializable]
    public class UserDataResultSave
    {
        public int UserId;
        public bool GameResult;
        public bool MissionClear;
        //note: アイテムなどはイベントで送る
    }

    public class APIResponce
    {
        public int Status;
    }

    public enum GameOption
    {
        None = 0,
        Recording = (1<<0),
    }
}