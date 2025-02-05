using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemInventry : MonoBehaviour
{
    [SerializeField] ItemDataBase _itemDataBase;
    public List<ItemList> _itemDataBases;
    ItemPanel _itemPanel;
    [SerializeField, Header("��̃A�C�e��ID���i�[")] private int[] _unionItemIDs = new int[2];
    private int _pushKeyNum;

    [SerializeField] public Button[] _itemButton;//�{�^���Ή��ւ̒ǋL��

    IvyEnent _IvyEnent; //�ӃC�x���g

    private KeyCode[] _key = new KeyCode[]
{
    KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2,
    KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
    KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8,
    KeyCode.Alpha9
};
    private void Awake()
    {
        if(FindObjectsOfType<ItemInventry>().Length > 2)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _itemPanel = FindObjectOfType<ItemPanel>();
        _IvyEnent = FindObjectOfType<IvyEnent>();
        
    }
    private void Update()
    {
        //IsNumberKeyDown();
    }
    public void PanelChange(int itemCount)
    {
        _itemPanel.ItemSlot(itemCount);
    }
    public void ItemUnion()
    {
        if (_unionItemIDs[0] == 0 || _unionItemIDs[1]== 0) { return; }
        else
        {
            int unionCount = _unionItemIDs[0] * _unionItemIDs[1];
            for(int i = 0; i < _itemDataBase._ItemLists.Count; i++)
            {
                if(unionCount == _itemDataBase._ItemLists[i]._itemID2)
                {
                    _itemDataBases.Add(_itemDataBase._ItemLists[i]);
                    ItemSearch(_unionItemIDs[0]);
                    ItemSearch(_unionItemIDs[1]);
                    _unionItemIDs[0] = 0;
                    _unionItemIDs[1] = 0;
                    PanelChange(_itemDataBases.Count);
                    break;
                }
                else if(i == _itemDataBase._ItemLists.Count - 1)
                {
                    _unionItemIDs[0] = 0;
                    _unionItemIDs[1] = 0;
                    Debug.Log("�����ł��܂���");
                }
            }
        }
    }
    public void IsNumberKeyDown()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < _key.Length; i++)
            {
                if (Input.GetKeyDown(_key[i]))
                {
                    _pushKeyNum = i;
                    if (ItemCheck(_pushKeyNum,_itemDataBases.Count))
                    {
                        Debug.Log(_itemDataBases[_pushKeyNum]._itemID);
                        ItemIDCheck(_unionItemIDs,_itemDataBases[_pushKeyNum]._itemID);
                        break;
                    }
                }
            }
        }
    }

    public void ButtonReturnTest(Button clickedButton) //�{�^���̃N���b�N�ւ̑Ή��@�ǋL��
    {
        int index = Array.IndexOf(_itemButton, clickedButton);

        Debug.Log("Button " + index + " clicked!");

        _pushKeyNum = index;

        //�����ӂ͈̔͂ɓ����Ă��ăA�C�e�����g�p�����ꍇ
        if (ItemCheck(_pushKeyNum, _itemDataBases.Count)�@&& _IvyEnent._playerTrigger)
        {
            _IvyEnent.AnswerNumberCheck(_itemDataBases[_pushKeyNum]._itemID);
        }
        else if(ItemCheck(_pushKeyNum, _itemDataBases.Count))
        {
            Debug.Log(_itemDataBases[_pushKeyNum]._itemID); //�{�^���ɓ����Ă���A�C�e����Id���o��
            ItemIDCheck(_unionItemIDs, _itemDataBases[_pushKeyNum]._itemID);
        }
    }

    public bool ItemCheck(int keyCount, int itemCount)
    {
        if (keyCount < itemCount) { return true; }
        else { return false; }
    }

    public void ItemIDCheck(int[] items, int itemID)
    {
        if (0 <= Array.IndexOf(items, itemID)) { items[Array.IndexOf(items, itemID)] = 0; _itemPanel._itemEffectText.text = ""; _itemButton[_pushKeyNum].image.color = Color.white; }
        else if (items[0] == 0) { items[0] = _itemDataBases[_pushKeyNum]._itemID; _itemPanel._itemEffectText.text = _itemDataBases[_pushKeyNum]._itemEffect;_itemButton[_pushKeyNum].image.color = Color.red; }
        else if (items[1] == 0 && items[0] != 0) { items[1] = _itemDataBases[_pushKeyNum]._itemID; _itemPanel._itemEffectText.text = _itemDataBases[_pushKeyNum]._itemEffect; _itemButton[_pushKeyNum].image.color = Color.red; }
    }

    public void ItemSearch(int itemID)
    {
        for(int i = 0; i < _itemDataBases.Count; i++)
        {
            if(itemID == _itemDataBases[i]._itemID) { _itemDataBases.RemoveAt(i); }
        }
    }

    public void ClosePanel()
    {
        GameManager.Instance.StateChange(GameManager.SystemState.Move);
    }
}
