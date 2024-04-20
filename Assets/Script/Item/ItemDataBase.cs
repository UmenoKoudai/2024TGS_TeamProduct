using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDataBase : ScriptableObject
{
    public List<ItemList> _ItemLists = new List<ItemList>();
}
