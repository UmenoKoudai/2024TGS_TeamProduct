using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VTNConnect
{
    /// <summary>
    /// ステータスコード
    /// </summary>
    public enum VC_StatusCode
    {
        //OK
        OK = 0, //OK

        //Error
        NetworkError = 101,     //ネットワーク環境に問題がある
        SerrverError = 102,     //サーバ内エラーが発生している
    };


    /// <summary>
    /// バンタンコネクト メインインタフェース
    /// NOTE: すべてここからアクセスできます。
    /// </summary>
    public class VantanConnect
    {
        //公開インタフェース

        /// <summary>GameIDを返す</summary>
        static public int GameID => _instance._systemSave.GameID;

        /// <summary>環境変数を返す</summary>
        static public IEnvironment Environment => _instance._environment;

        /// <summary>システム変数を返す</summary>
        static public SystemSaveData SystemSave => _instance._systemSave;

        /// <summary>ゲーム中かどうかを返す</summary>
        static public bool IsInGame => _instance._gameStateSave.IsInGame;

        static public string GameHash => _instance._gameStateSave.GameHash;


        #region ゲームAPI

        /// <summary>システムを初期化</summary>
        static public void SystemReset() { _instance.SystemResetImplement(); }

        //非同期関数

#if AIGAME_IMPLEMENT
        /// <summary>ゲーム開始</summary>
        static async public UniTask<GameStartAIGameResult> GameStart() { return await _instance.GameStartImplement(); }

        /// <summary>ゲーム終了</summary>
        static async public UniTask<VC_StatusCode> GameEnd() { return await _instance.GameEndImplement(); }

        /// <summary>現在のAIゲームに参加しているユーザーリストを返す(GameStart後に取得可能)</summary>
        static public UserData[] GetMainGameUsers() { return _instance.GetMainGameUsersImplement(); }
        /// <summary>現在のAIゲームに参加しているユーザーリストを返す(GameStart後に取得可能)</summary>
        static public string GetGameTitle() { return _instance._gameStateSave.GameTitle; }

        /// <summary>生死を保存</summary>
        static public void UserRecord(int userId, bool gameResult, bool isMissionClear) { _instance.UserRecordImplement(userId, gameResult, isMissionClear); }
#else
        /// <summary>ゲーム開始</summary>
        static async public UniTask<VC_StatusCode> GameStart() { return await _instance.GameStartImplement(); }

        /// <summary>ゲーム終了</summary>
        static async public UniTask<VC_StatusCode> GameEnd(bool gameResult) { return await _instance.GameEndImplement(gameResult); }
#endif

        //コールバック関数
        public delegate void ExecuteCallback(VC_StatusCode code);   //戻りを受け取る関数
        delegate UniTask<VC_StatusCode> ExecuteFunction();                      //実行関数
        delegate UniTask<VC_StatusCode> ExecuteFunctionResult(bool gameResult); //実行関数


#if !AIGAME_IMPLEMENT
        /// <summary>ゲーム開始</summary>
        static public void GameStart(ExecuteCallback callback) { _instance.CallbackAction(callback, _instance.GameStartImplement); }

        /// <summary>ゲーム終了</summary>
        static public void GameEnd(bool gameResult, ExecuteCallback callback) { _instance.CallbackAction(callback, _instance.GameEndImplement, gameResult); }
#endif
        #endregion

        #region イベント
        /// <summary>イベントを受信するクラスを登録(何個登録しても良い)</summary>
        static public void RegisterEventReceiver(IVantanConnectEventReceiver receiver) { _instance.RegisterReceiver(receiver); }

        /// <summary>イベントを送信(イベントデータを作って送信)</summary>
        static public void SendEvent(EventData data) { _instance.SendVCEvent(data); }

        /// <summary>イベントを送信(IDのみで送信)</summary>
        static public void SendEvent(EventDefine eventId) { _instance.SendVCEvent(eventId); }


#if AIGAME_IMPLEMENT
#else
        /// <summary>ゲーム内物語の作成ヘルパ(物語データにUserIdを絡ませる)</summary>
        static public GameEpisode CreateEpisode(EpisodeCode epiCode) { return _instance.CreateVCEpisode(epiCode); }
#endif
        /// <summary>ゲーム内物語を送信(物語データを作って送信)</summary>
        static public void SendEpisode(GameEpisode episode) { _instance.SendVCEpisode(episode); }

#endregion


        #region 内部処理用
        static VantanConnect _instance = new VantanConnect();
        VantanConnect() { }

        //それぞれの処理委譲系
        EventSystem _eventSystem = new EventSystem();
        GameStateSave _gameStateSave = new GameStateSave();
        LinkageSyatem _linkageSystem = new LinkageSyatem();

        //セットアップ時に実態が生まれるもの
        IEnvironment _environment = null;
        WebSocketEventManager _wsManager = null;
        SystemViewer _systemView = null;
        SystemSaveData _systemSave = null;
        GameObject _vcMainObject = null;
        GameObject _vcOverlayObject = null;

        //APIリスト
        APIGetWSAddressImplement _getAddress = new APIGetWSAddressImplement();
        APIGetUserImplement _getUser = new APIGetUserImplement();
        APIGetGameUsersImplement _getGameUsers = new APIGetGameUsersImplement();


        //内部インタフェース

        //エントリポイント(ゲーム開始時にコールされる)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Run()
        {
            Debug.Log("VantanConnect Setup Ready");

            //システムセーブのロード
            var systemSave = LocalData.Load<SystemSaveData>("SystemSave.json");
            if(systemSave == null)
            {
                systemSave = new SystemSaveData();
            }
            _instance._systemSave = systemSave;

            //環境構成
            switch(systemSave.Environment)
            {
                case EnvironmentSetting.Local:
                    _instance._environment = new LocalEnvironment();
                    break;

                case EnvironmentSetting.Develop:
                case EnvironmentSetting.Production:
                    _instance._environment = new ProductionEnvironment();
                    break;
            }

            //常駐する管理オブジェクトの生成
            GameObject obj = new GameObject("VCMain");
            _instance._wsManager = obj.AddComponent<WebSocketEventManager>();
            _instance._systemView = obj.AddComponent<SystemViewer>();
            GameObject.DontDestroyOnLoad(obj);
            _instance._vcMainObject = obj;

            //VCオブジェクトを作成する
            //TODO: Addressablesに移行したいが、そこまで必要性を感じないのでこのまま
            var prefab = Resources.Load<GameObject>("ConnectEventAssets/Prefabs/VC_Overlay");
            var overlay = GameObject.Instantiate(prefab);
            GameObject.DontDestroyOnLoad(overlay);
            _instance._vcOverlayObject = overlay;
            _instance._linkageSystem.Setup(overlay.GetComponentInChildren<VC_LoginView>());

            //イベント登録系など
            _instance._eventSystem.SetEventSystem(_instance._wsManager);
            _instance._eventSystem.RegisterReceiver(_instance._linkageSystem);
            _instance._eventSystem.SystemInitialSave();

#if !AIGAME_IMPLEMENT
            if (_instance._systemSave.IsDebugSceneLaunch)
            {
                SystemReset();
            }
#endif
        }

        // ゲーム管理系 ///////////////////

        /// <summary>
        /// システム初期化
        /// NOTE: 各種システムを起動時の状態に戻す
        /// </summary>
        void SystemResetImplement()
        {
            _linkageSystem.Reset();
            _eventSystem.Reset();
        }

        /// <summary>
        /// ゲーム開始実装
        /// NOTE: AIかいっぱいゲーム化で内部処理は分かれる
        /// </summary>
#if AIGAME_IMPLEMENT
        async UniTask<GameStartAIGameResult> GameStartImplement()
        {
            return await _gameStateSave.GameStartAIGame();
        }
#else
        async UniTask<VC_StatusCode> GameStartImplement()
        {
            return await GameStartVCGame();
        }
#endif

    /// <summary>
    /// ゲーム終了実装
    /// </summary>
#if AIGAME_IMPLEMENT
    async UniTask<VC_StatusCode> GameEndImplement()
        {
            return await _gameStateSave.GameEndAIGame();
        }
#else
        async UniTask<VC_StatusCode> GameEndImplement(bool gameResult)
        {
            return await _gameStateSave.GameEndVCGame(gameResult);
        }
#endif
        void CallbackAction(ExecuteCallback func, ExecuteFunction action)
        {
            action?.Invoke().ContinueWith((VC_StatusCode c) => func?.Invoke(c));
        }

        void CallbackAction(ExecuteCallback func, ExecuteFunctionResult action, bool gameResult)
        {
            action?.Invoke(gameResult).ContinueWith((VC_StatusCode c) => func?.Invoke(c));
        }

#if AIGAME_IMPLEMENT
        UserData[] GetMainGameUsersImplement()
        {
            return _gameStateSave.Users;
        }

        void UserRecordImplement(int userId, bool gameResult, bool isMissionClear)
        {
            _gameStateSave.StackUser(userId, gameResult, isMissionClear);
        }
#else
        async UniTask<VC_StatusCode> GameStartVCGame()
        {
            GameStartRequest request = new GameStartRequest();
            request.GameId = VantanConnect.GameID;
            request.UserId = 0;

            //特殊なステータス
            if (_systemSave.IsRecording)
            {
                request.Option |= (int)GameOption.Recording;
            }

            //ユーザがリンクしていればそのユーザのUserIdを送信する
            if(_linkageSystem.IsLink)
            {
                request.UserId = _linkageSystem.UserData.UserId;
            }

            //リンクビュー非表示にする
            _linkageSystem.SetViewEnable(false);

            return await _gameStateSave.GameStartVCGame(request);
        }

        async UniTask<VC_StatusCode> GameEndVCGame(bool gameResult)
        {
            return await _gameStateSave.GameEndVCGame(gameResult);
        }
#endif

        //イベント系
        void RegisterReceiver(IVantanConnectEventReceiver receiver)
        {
            _eventSystem.RegisterReceiver(receiver);
        }

        void SendVCEvent(EventDefine evCode)
        {
            EventData data = new EventData(evCode);
            SendVCEvent(data);
        }

#if AIGAME_IMPLEMENT
#else
        GameEpisode CreateVCEpisode(EpisodeCode epiCode)
        {
            if (!_linkageSystem.IsLink)
            {
                return new GameEpisode(epiCode, 0);
            }
            
            return new GameEpisode(epiCode, _instance._linkageSystem.UserData.UserId);
        }
#endif
        void SendVCEvent(EventData d)
        {
            _wsManager.Send(d);
        }

        void SendVCEpisode(GameEpisode d)
        {
            if (!d.IsValidEpisode) return;

            _wsManager.Send(d);
        }
#endregion
    }
}