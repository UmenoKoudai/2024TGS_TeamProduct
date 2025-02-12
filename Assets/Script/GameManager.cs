using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("�C���X�y�N�^�[�ł��肢���܂�")]
    //�t���O�f�[�^
    [SerializeField] FlagList _flagList;
    public FlagList FlagList => _flagList;
    [SerializeField, Tooltip("���̃V�[�����A�E�g�Q�[�����C���Q�[�����I��")]
    private GameState _nowState;
    [SerializeField, Tooltip("�v���C���[���A�^�b�`")]
    private Player _player;
    public Player Player => _player;
    [SerializeField, Tooltip("�������A�^�b�`")]
    private Girl _girl;
    public Girl Girl => _girl;
    [SerializeField, Tooltip("�C�x���g�}�l�[�W���[���A�^�b�`")]
    private EventManager _eventManager;
    public EventManager EventManager => _eventManager;
    [SerializeField, Tooltip("�p�l���}�l�[�W���[���A�^�b�`")]
    private PanelManager _panelManager;
    public PanelManager PanelManager => _panelManager;
    [SerializeField, Tooltip("�C�x���g�V�X�e�����A�^�b�`")]
    private EventSystem _eventSystem;
    public EventSystem EventSystem => _eventSystem;
    [SerializeField]
    private FlagData _girlFlag;
    [SerializeField]
    private GameObject _savepanel;


    enum GameState
    {
        OutGame,
        InGame,
    }

    #region�@�V���O���g��

    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
                if (!instance)
                {
                    Debug.LogError("�Q�[���}�l�[�W���[�����݂��܂���");
                }
            }

            return instance;
        }
    }

    //void Awake()
    //{
    //    if (!instance)
    //    {
    //        instance = this;
    //       //SceneManager.sceneLoaded += SceneLoaded;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //static void SetupInstance()
    //{
    //    instance = FindObjectOfType<GameManager>();

    //    if (!instance)
    //    {
    //        GameObject go = new GameObject();
    //        instance = go.AddComponent<GameManager>();
    //        go.name = instance.GetType().Name;
    //        DontDestroyOnLoad(go);
    //    }
    //}
    #endregion

    #region�@�X�e�[�g�}�V�[��
    public enum SystemState
    {
        Move,
        Talk,
        Option,
        SceneMove,
        Select,
        Save,
        GameOver,

        MacCount,
    }

    private SystemState _state;
    public SystemState State
    {
        get => _state;
        set
        {
            if (_state == value) return;
            _state = value;
            _currentState = _states[(int)_state];
            _currentState.Enter();
        }
    }

    private IStateMachine[] _states = new IStateMachine[(int)SystemState.MacCount];
    private IStateMachine _currentState;
    #endregion

    void Start()
    {
        if (_nowState == GameState.OutGame) return;
        Init();
        _girl.gameObject.SetActive(_girlFlag.IsOn);
        _states[(int)SystemState.Move] = new MoveState(this);
        _states[(int)SystemState.Talk] = new TalkState(this);
        _states[(int)SystemState.Option] = new OptionState(this);
        _states[(int)SystemState.Select] = new SelectState(this);
        _states[(int)SystemState.GameOver] = new GameOverState(this);
        _currentState = _states[(int)_state];
    }

    private void Update()
    {
        if (_nowState == GameState.OutGame) return;
        try
        {
            _currentState.Update();
        }
        catch
        {
            Debug.LogError($"�X�e�[�g���ݒ肳��Ă��܂���{this.gameObject.GetType()}:{name}");
        }
    }

    private void Init()
    {
        try
        {
            if (PlayingData.Instance.PosName != "" && PlayingData.Instance.PosName != null)
            {
                var pos = GameObject.Find(PlayingData.Instance.PosName).transform.position + (PlayingData.Instance.Direction * 3);
                _player.Direction = PlayingData.Instance.Direction;
                _girl.Direction = PlayingData.Instance.Direction;
                _player.transform.position = pos;
                _girl.transform.position = pos;
            }
        }
        catch
        {
            Debug.LogError($"{PlayingData.Instance.PosName}�����݂��܂���");
        }
        _player.Init(_eventManager);
        _girl.Init(_eventManager);
    }

    public void StateChange(SystemState change)
    {
        State = change;
        if (change != SystemState.Move)
        {
            _player.Rb.velocity = Vector2.zero;
            _girl.Rb.velocity = Vector2.zero;
        }
    }

    public void Save()
    {
        FindObjectOfType<SaveLoadManager>().OpenSavePanel();
    }

    public void Load()
    {
        FindObjectOfType<SaveLoadManager>().OpenLoadPanel();
    }

    public void Close()
    {
        _panelManager.SelectPanel.SetActive(false);
    }
}
