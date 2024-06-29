using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : CharacterBase
{
    [Header("�f�o�b�N�p")]
    [SerializeField]
    private bool _isDebug = false;
    [SerializeField]
    GameObject _obj;

    public override void CreatePos(Vector3 pos)
    {
        if(_isDebug )Instantiate(_obj,pos, Quaternion.identity);
    }
}
