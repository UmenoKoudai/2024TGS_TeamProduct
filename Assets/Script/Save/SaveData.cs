//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Linq;
//using System.IO;

//[Serializable]
//public class SaveData : MonoBehaviour
//{
//    [Header("セーブする値の確認")]
//    Transform _savePos;
//    bool[] _flags;
//    string _nowTime = "";
//    Date _date;

//    public class Date
//    {
//        public Transform SavePos;
//        public string NowTime;
//        public bool[] Flag;
//    }

//    public void Display()
//    {
//        Debug.Log($"Pos{_savePos}");
//        Debug.Log($"Flag{string.Join("", _flags)}");
//        Debug.Log($"TimeDate{_nowTime}");
//    }

//    public string GetDateTime()
//    {
//        return _nowTime;
//    }
//}