using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]



































public class SaveData : MonoBehaviour
{
    [Header("�Z�[�u����l�̊m�F")]
    //[SerializeField]�����ĕۑ��������l��錾
    //���݂̓Z�[�u�n�_�A�L�����N�^�[��san�l�A�C�x���g�̃t���O
    Transform _savePos;
    int _sanNum;
    List<bool> _flags;
    string _nowTime;

    //�Z�[�u����Ƃ��ɒl��������
    public void Save()
    {
        //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
        _savePos = FindObjectOfType<Player>().transform;

        var dateTime = DateTime.Now;
        _nowTime = $"{dateTime.Year}/{dateTime.Month}/{dateTime.Day} {dateTime.Hour}:{dateTime.Minute}";

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
        try
        {
            //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
            FindObjectOfType<Player>().transform.position = _savePos.position;
            //san�l����
            //
            //�t���O����
            for (int i = 0; i < _flags.Count; i++)
            {
                GameManager.Instance.FlagList.SetFlag(GameManager.Instance.FlagList.Flags[i], _flags[i]);
            }
        }
        catch
        {
            Debug.LogError("�f�[�^�����݂��܂���");   
        }
    }

    public string GetDateTime()
    {
        return _nowTime;
    }
}