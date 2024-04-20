using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ItemList
{
    [SerializeField, Header("�A�C�e����id")]
    public int _itemID;
    [SerializeField, Header("�A�C�e����2�ڂ�id �����p")]
    public int _itemID2;
    [SerializeField, Header("�A�C�e���̖��O")]
    public string _itemName;
    [SerializeField, Header("�A�C�e����Sprite")]
    public Sprite _itemSprite;
    [SerializeField, Header("�A�C�e���̐���")]
    public string _itemEffect;
    [SerializeField, Header("�A�C�e���̃v���n�u")]
    public GameObject _item;
}