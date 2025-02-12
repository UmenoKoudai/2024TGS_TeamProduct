using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("インスペクターでお願いします")]
    //フラグデータ
    [SerializeField] FlagList _flagList;
    public FlagList FlagList => _flagList;
    [SerializeField, Tooltip("このシーンがアウトゲームかインゲームか選択")]
    private GameState _nowState;
    [SerializeField, Tooltip("プレイヤーをアタッチ")]
    private Player _player;
    public Player Player => _player;
    [SerializeField, Tooltip("少女をアタッチ")]
    private Girl _girl;
    public Girl Girl => _girl;
    [SerializeField, Tooltip("イベントマネージャーをアタッチ")]
    private EventManager _eventManager;
    public EventManager EventManager => _eventManager;
    [SerializeField, Tooltip("パネルマネージャーをアタッチ")]
    private PanelManager _panelManager;
    public PanelManager PanelManager => _panelManager;
    [SerializeField, Tooltip("イベントシステムをアタッチ")]
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

    #region　シングルトン

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
                    Debug.LogError("ゲームマネージャーが存在しません");
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

    #region　ステートマシーン
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
            Debug.LogError($"ステートが設定されていません{this.gameObject.GetType()}:{name}");
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
            Debug.LogError($"{PlayingData.Instance.PosName}が存在しません");
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
