using System;

/// <summary>
/// スプレッドシートからダウンロードしてくるデータたち
/// </summary>
namespace DataManagement
{
    /// <summary>
    /// システム用
    /// </summary>
    [Serializable]
    public class SpreadSheetMasterVersion
    {
        public string SheetName;
        public int Version;
    }

    [Serializable]
    public class SpreadSheetDataObject
    {
        public int Version;
        //xxx[] Data;
    }

    [Serializable]
    public class MasterDataVersion
    {
        public long TimeStamp;
        public SpreadSheetMasterVersion[] Data;
    }

    namespace SpreadSheet
    {
        /// <summary>
        /// テキストデータ
        /// </summary>
        [Serializable]
        public class TextData
        {
            public string Key;
            public string Text;
        }

        [Serializable]
        public class TextMaster : SpreadSheetDataObject
        {
            public TextData[] Data;
        }
    }
}