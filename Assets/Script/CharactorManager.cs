using UnityEngine;

public class CharactorManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Girl _girl;
    [SerializeField]
    private EventManager _eventManager;

    private PlayCharacter _playChara = PlayCharacter.Player;

    enum PlayCharacter
    {
        Player,
        Girl,
    }

    void Start()
    {
   
    }

    void Update()
    {
        if (_playChara == PlayCharacter.Player)
        {
            _player.ManualUpdate();
            _player.ManualFixedUpdate();
        }
        else
        {
            _girl.ManualUpdate();
            _girl.ManualFixedUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(_playChara == PlayCharacter.Player)
            {
                _playChara = PlayCharacter.Girl;
            }
            else
            {
                _playChara = PlayCharacter.Player;
            }
        }
    }
}
