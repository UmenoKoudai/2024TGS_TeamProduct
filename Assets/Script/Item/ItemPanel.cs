using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemPanel : MonoBehaviour
{
    ItemInventry _itemInventry;
    [SerializeField] Image[] _images;
    // Start is called before the first frame update
    void Start()
    {
        _itemInventry = FindObjectOfType<ItemInventry>();
    }
    public void ItemSlot(int count)
    {
        if (count <= _images.Length)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].sprite = null;
            }
            for (int i = 0; i < count; i++)
            {
                _images[i].sprite = _itemInventry._itemDataBases[i]._itemSprite;
            }
        }

        //ƒ{ƒ^ƒ“‚Ì‘Î‰ž‚Ö‚Ì’Ç‹L•¶

        if (count <= _itemInventry._itemButton.Length)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _itemInventry._itemButton[i].image.sprite = null;
            }
            for (int i = 0; i < count; i++)
            {
                _itemInventry._itemButton[i].image.sprite = _itemInventry._itemDataBases[i]._itemSprite;
            }
        }
    }
}
