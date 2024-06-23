using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [Header("�Z�[�u����l�̊m�F")]
    [SerializeField] Transform _savePos;
    [SerializeField] List<bool> _flags;
    [SerializeField] string _sceneName;
    [SerializeField] List<bool> _items;

    //�Z�[�u����Ƃ��ɒl��������
    public void Save()
    {
        //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
        _savePos = FindObjectOfType<Player>().transform;
        _sceneName = SceneManager.GetActiveScene().name;

        //�t���O����
        _flags = new List<bool>();
        for (int i = 0; i < GameManager.Instance.FlagList.Flags.Count; i++)
        {
            _flags.Add(GameManager.Instance.FlagList.GetFlagStatus(GameManager.Instance.FlagList.Flags[i]));
        }
    }

    //���[�h����Ƃ��ɒl��������
    public void Load()
    {
        //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
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
            { Debug.LogError("�v���C���[����"); }
        }

        //�t���O����
        for (int i = 0; i < _flags.Count; i++)
        {
            GameManager.Instance.FlagList.SetFlag(GameManager.Instance.FlagList.Flags[i], _flags[i]);
        }
    }
}