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
    [SerializeField] Vector2 _savePos;
    [SerializeField] int _sanNum;
    [SerializeField] List<bool> _flags;

    //�Z�[�u����Ƃ��ɒl��������
    public void Save()
    {
        //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
        //_savePos = ;

        //san�l����
        //_sanNum = ;

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
        //
        //san�l����
        //
        //�t���O����
        for (int i = 0; i < _flags.Count; i++)
        {
            GameManager.Instance.FlagList.SetFlag(GameManager.Instance.FlagList.Flags[i], _flags[i]);
        }
    }
}