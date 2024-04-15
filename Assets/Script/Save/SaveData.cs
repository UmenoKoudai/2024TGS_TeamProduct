using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData : MonoBehaviour
{
    [Header("インスペクターでお願いします")]
    //フラグデータ
    [SerializeField] FlagList _flagList;

    [Header("セーブする値の確認")]
    //[SerializeField]をつけて保存したい値を宣言
    //現在はセーブ地点、キャラクターのsan値、イベントのフラグ
    [SerializeField] Vector2 _savePos;
    [SerializeField] int _sanNum;
    [SerializeField] List<bool> _flags;

    //セーブするときに値を代入する
    public void Save()
    {
        //セーブ地点、キャラクターの位置
        //_savePos = ;
        //san値を代入
        //_sanNum = ;
        //フラグを代入
        _flags = new List<bool>();
        for (int i = 0; i < _flagList.Flags.Count; i++)
        {
            _flags.Add(_flagList.GetFlagStatus(_flagList.Flags[i]));

        }
    }

    //ロードするときに値を代入する
    public void Load()
    {
        //セーブ地点、キャラクターの位置
        //
        //san値を代入
        //
        //フラグを代入
        for (int i = 0; i < _flags.Count; i++)
        {
            _flagList.SetFlag(_flagList.Flags[i], _flags[i]);
        }
    }
}