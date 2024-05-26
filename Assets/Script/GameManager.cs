using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("インスペクターでお願いします")]
    //フラグデータ
    [SerializeField] FlagList _flagList;
    public FlagList FlagList => _flagList;

    #region　シングルトン

    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                SetupInstance();
            }

            return instance;
        }
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            SceneManager.sceneLoaded += SceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    static void SetupInstance()
    {
        instance = FindObjectOfType<GameManager>();

        if (!instance)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<GameManager>();
            go.name = instance.GetType().Name;
            DontDestroyOnLoad(go);
        }
    }
    #endregion

    public enum SystemState
    {
        Move,
        Talk,
        Option,
        SceneMove,
    }

    private SystemState _state;
    public SystemState State 
    {
        get => _state;
        set
        {
            if (_state == value) return;
            _state = value;
            switch (_state)
            {
                case SystemState.Move:
                    _moveState.Enter();
                    break;
                case SystemState.Talk:
                    _talkState.Enter();
                    break;
                case SystemState.Option:
                    _optionState.Enter();
                    break;
            }
        }
    }

    private string _posName = "";
    public string PosName { get => _posName; set => _posName = value; }
    private Vector3 _direction;
    public Vector3 Direction { get => _direction; set => _direction = value; }
    private Player _player;
    public Player Player => _player;
    private Girl _girl;
    public Girl Girl => _girl;
    private EventManager _eventManager;
    public EventManager EventManager => _eventManager;
    private PanelManager _panelManager;
    public PanelManager PanelManager => _panelManager;

    private MoveState _moveState;
    private TalkState _talkState;
    private OptionState _optionState;

    void Start()
    {
        Debug.Log("Start");
        _player.Init(_eventManager);
        _girl.Init(_eventManager);
        _moveState = new MoveState(this);
        _talkState = new TalkState(this);
        _optionState = new OptionState(this);
    }

    private void Update()
    {
        switch(_state)
        {
            case SystemState.Move:
                _moveState.Update();
                break;
            case SystemState.Talk:
                _talkState.Update();
                break;
            case SystemState.Option:
                _optionState.Update();
                break;
        }
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _player = FindObjectOfType<Player>();
        _girl = FindObjectOfType<Girl>();
        _eventManager = FindObjectOfType<EventManager>();
        _panelManager = FindObjectOfType<PanelManager>();
        if (PosName != "")
        {
            var pos = GameObject.Find(PosName).transform.position;
            if (_player && _girl)
            {
                _player.Init(_eventManager);
                _girl.Init(_eventManager);
                _player.transform.position = pos;
                _girl.transform.position = pos;
                _player.transform.up = Direction;
                _girl.transform.up = Direction;
            }
        }
        StateChange(SystemState.Move);
    }

    public void StateChange(SystemState change)
    {
        State = change;
    }
}
