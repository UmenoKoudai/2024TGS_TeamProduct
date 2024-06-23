using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [Header("セーブする値の確認")]
    [SerializeField] Transform _savePos;
    [SerializeField] List<bool> _flags;
    [SerializeField] string _sceneName;
    [SerializeField] List<bool> _items;

    //セーブするときに値を代入する
    public void Save()
    {
        //セーブ地点、キャラクターの位置
        _savePos = FindObjectOfType<Player>().transform;
        _sceneName = SceneManager.GetActiveScene().name;

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
        //セーブ地点、キャラクターの位置
        SceneManager.LoadScene(_sceneName);
        GameObject player = FindObjectOfType<Player>().gameObject;
        if (player != null)
        {
            player.transform.position = _savePos.position;
        }
        else
        {
            if (_player != null)
            { Instantiate(_player, _savePos.position, Quaternion.identity); }
            else
            { Debug.LogError("プレイヤーを代入"); }
        }

        //フラグを代入
        for (int i = 0; i < _flags.Count; i++)
        {
            GameManager.Instance.FlagList.SetFlag(GameManager.Instance.FlagList.Flags[i], _flags[i]);
        }
    }
}