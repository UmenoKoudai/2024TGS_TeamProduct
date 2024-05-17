using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] ItemDataBase _itemDataBase;
    [SerializeField] string _itemName;
    ItemInventry _ItemInventry;

    public void PickUp()
    {
        Debug.Log("アイテムゲット");
        _ItemInventry = FindObjectOfType<ItemInventry>();
        for(int i = 0; i < _itemDataBase._ItemLists.Count; i++)
        {
            if(_itemName == _itemDataBase._ItemLists[i]._itemName)
            {
                int itemID = _itemDataBase._ItemLists[i]._itemID;
                if(itemID == _itemDataBase._ItemLists[i]._itemID)
                {
                    _ItemInventry._itemDataBases.Add(_itemDataBase._ItemLists[i]);
                    _ItemInventry.PanelChange(_ItemInventry._itemDataBases.Count);
                    break;
                }
            }
        }
    }
}