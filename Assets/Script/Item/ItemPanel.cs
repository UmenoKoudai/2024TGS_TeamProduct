using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    ItemInventry _itemInventry;
    [SerializeField] Image[] _images;
    [SerializeField]public Text _itemEffectText;

    [SerializeField, Header("文字数のマジックナンバー")] int _num = 6;
    // Start is called before the first frame update
    void Start()
    {
        _itemInventry = FindObjectOfType<ItemInventry>();
    }
    public void ItemSlot(int count)
    {
        //if (count <= _images.Length)
        //{
        //    for (int i = 0; i < _images.Length; i++)
        //    {
        //        _images[i].sprite = null;
        //    }
        //    for (int i = 0; i < count; i++)
        //    {
        //        _images[i].sprite = _itemInventry._itemDataBases[i]._itemSprite;
        //    }
        //}

        //ボタンの対応への追記文

        //if (count <= _itemInventry._itemButton.Length)
        //{
        //    for (int i = 0; i < _itemInventry._itemButton.Length; i++)
        //    {
        //        _itemInventry._itemButton[i].image.sprite = null;
        //    }
        //    for (int i = 0; i < count; i++)
        //    {
        //        _itemInventry._itemButton[i].image.sprite = _itemInventry._itemDataBases[i]._itemSprite;
        //    }
        //}

        //ボタン対応への名前変更
        if (count <= _itemInventry._itemButton.Length)
        {
            for (int i = 0; i < _itemInventry._itemButton.Length; i++)
            {
                //_itemInventry._itemButton[i].image.sprite = null;
                Text text = _itemInventry._itemButton[i].GetComponentInChildren<Text>();
                text.text = "";
            }
            for (int i = 0; i < count; i++)
            {
                Text text = _itemInventry._itemButton[i].GetComponentInChildren<Text>();
                text.text = _itemInventry._itemDataBases[i]._itemName;

                int itemNum = _itemInventry._itemDataBases[i]._itemName.Length;

                if(itemNum > _num)
                {
                    int down = itemNum - _num;
                    text.fontSize -= down;

                }
            }
        }
    }
}
