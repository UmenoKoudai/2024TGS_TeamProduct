using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]



































public class SaveData : MonoBehaviour
{
    [Header("セーブする値の確認")]
    //[SerializeField]をつけて保存したい値を宣言
    //現在はセーブ地点、キャラクターのsan値、イベントのフラグ
    Transform _savePos;
    int _sanNum;
    List<bool> _flags;
    string _nowTime;

    //セーブするときに値を代入する
    public void Save()
    {
        //セーブ地点、キャラクターの位置
        _savePos = FindObjectOfType<Player>().transform;

        var dateTime = DateTime.Now;
        _nowTime = $"{dateTime.Year}/{dateTime.Month}/{dateTime.Day} {dateTime.Hour}:{dateTime.Minute}";

        //フラグを代入
        _flags = new List<bool>();
        for (int i = 0; i < GameManager.Instance.FlagList.Flags.Count; i++)
        {
            _flags.Add(GameManager.Instance.FlagList.GetFlagStatus(GameManager.Instance.FlagList.Flags[i]));

        }
    }

    //ロードするときに値を代入する
    public void Load()
    {
        try
        {
            //セーブ地点、キャラクターの位置
            FindObjectOfType<Player>().transform.position = _savePos.position;
            //san値を代入
            //
            //フラグを代入
            for (int i = 0; i < _flags.Count; i++)
            {
                GameManager.Instance.FlagList.SetFlag(GameManager.Instance.FlagList.Flags[i], _flags[i]);
            }
        }
        catch
        {
            Debug.LogError("データが存在しません");   
        }
    }

    public string GetDateTime()
    {
        return _nowTime;
    }
}