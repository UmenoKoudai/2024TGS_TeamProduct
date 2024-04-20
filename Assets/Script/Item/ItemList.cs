using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ItemList
{
    [SerializeField, Header("アイテムのid")]
    public int _itemID;
    [SerializeField, Header("アイテムの2つ目のid 合成用")]
    public int _itemID2;
    [SerializeField, Header("アイテムの名前")]
    public string _itemName;
    [SerializeField, Header("アイテムのSprite")]
    public Sprite _itemSprite;
    [SerializeField, Header("アイテムの説明")]
    public string _itemEffect;
    [SerializeField, Header("アイテムのプレハブ")]
    public GameObject _item;
}