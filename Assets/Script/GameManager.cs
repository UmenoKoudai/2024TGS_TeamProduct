using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("インスペクターでお願いします")]
    //フラグデータ
    [SerializeField] FlagList _flagList;
    public FlagList FlagList => _flagList;
    [SerializeField] 
    private Player _player;
    public Player Player => _player;
    [SerializeField]
    private Girl _girl;
    public Girl Girl => _girl;
    [SerializeField]
    private EventManager _eventManager;
    [SerializeField]
    private GameObject _talkPanel;
    public GameObject TalkPanel => _talkPanel;
    [SerializeField]
    private GameObject _optionPanel;
    public GameObject OptionPanel => _optionPanel;
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
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded += SceneLoaded;
            return;
        }

        DontDestroyOnLoad(gameObject);
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

    public Transform BasePos { get; set; }

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
        var player = FindObjectOfType<Player>();
        var girl = FindObjectOfType<Girl>();
        player.transform.position = BasePos.transform.position;
        girl.transform.position = BasePos.transform.position;
    }

    public void StateChange(SystemState change)
    {
        State = change;
    }
}
