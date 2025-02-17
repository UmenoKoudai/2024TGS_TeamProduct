using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// 冒険者とリンクする仕組み
    /// </summary>
    public class LinkageSyatem : IVantanConnectEventReceiver
    {
        public bool IsActive => true;
        public bool IsLink => _user != null;

        public UserData UserData => _user;

        APIGetUserImplement _getUser = new APIGetUserImplement();
        APIGameHandOverImplement _gameHandOverUser = new APIGameHandOverImplement();
        UserData _user = null;
        VC_LoginView _view = null;

        public enum VC_LinkageEvent
        {
            Link = 1000,    //リンク
            ReLink = 1005,  //交代
        }

        public void Setup(VC_LoginView view)
        {
            _view = view;
            SetViewEnable(false);
        }

        public void Reset()
        {
            //状態をリセット
            _user = null;

#if !AIGAME_IMPLEMENT
            if (!VantanConnect.SystemSave.IsDebugSceneLaunch)
            {
                SetViewEnable(true);
            }
            
            //リセットをフックにデバッグ処理をコール
            //コネクト処理を常時行う
            //ゲームステート保全のため、常にリリンク処理を使用する
            if(VantanConnect.SystemSave.IsDebugConnect)
            {
                UniTask.RunOnThreadPool(async () =>
                {
                    int userId = VantanConnect.SystemSave.UseConnectUserId;
                    if (userId == 0) return;

                    GameHandOverRequest req = new GameHandOverRequest()
                    {
                        GameId = VantanConnect.GameID,
                        UserId = userId
                    };

                    //特殊なステータス
                    if (VantanConnect.SystemSave.IsRecording)
                    {
                        req.Option |= (int)GameOption.Recording;
                    }

                    //GameStartとGameEndとUserGet
                    var result = await _gameHandOverUser.Request(req);
                    var status = APIUtility.PacketCheck(result);
                    if (status != VC_StatusCode.OK)
                    {
                        Debug.LogError("エラーです");
                        return;
                    }
                    _user = result.UserData;

                    await UniTask.SwitchToMainThread();
                    _view.Link(_user.DisplayName);
                }).Forget();
            }
#endif
        }

        public void SetViewEnable(bool isEnableView)
        {
            _view?.SetEnable(isEnableView);
        }

        //チェインするデータを受け取り処理する
        public void OnEventCall(EventData data)
        {
#if !AIGAME_IMPLEMENT
            switch ((VC_LinkageEvent)data.EventId)
            {
                case VC_LinkageEvent.Link:
                    {
                        Debug.Log(JsonUtility.ToJson(data));
                        var gameId = data.GetIntData("GameId");
                        if (gameId != VantanConnect.GameID) break;

                        UniTask.RunOnThreadPool(async () =>
                        {
                            var result = await _getUser.Request(data.GetIntData("UserId"));
                            var status = APIUtility.PacketCheck(result);
                            if (status != VC_StatusCode.OK)
                            {
                                Debug.LogError("エラーです");
                                return;
                            }

                            _user = result.UserData;

                            await UniTask.SwitchToMainThread();
                            _view.Link(_user.DisplayName);
                        }).Forget();
                    }
                    break;

                case VC_LinkageEvent.ReLink:
                    {
                        Debug.Log(JsonUtility.ToJson(data));
                        var gameId = data.GetIntData("GameId");
                        if (gameId != VantanConnect.GameID) break;

                        var userId = data.GetIntData("UserId");
                        if (userId == 0) break;

                        GameHandOverRequest req = new GameHandOverRequest()
                        {
                            GameId = gameId,
                            UserId = userId
                        };

                        //特殊なステータス
                        if (VantanConnect.SystemSave.IsRecording)
                        {
                            req.Option |= (int)GameOption.Recording;
                        }

                        UniTask.RunOnThreadPool(async () =>
                        {
                            //GameStartとGameEndとUserGet
                            var result = await _gameHandOverUser.Request(req);
                            var status = APIUtility.PacketCheck(result);
                            if (status != VC_StatusCode.OK)
                            {
                                Debug.LogError("エラーです");
                                return;
                            }
                            _user = result.UserData;

                            await UniTask.SwitchToMainThread();
                            /*
                            _view.Link(_user.DisplayName);
                            */
                        }).Forget();
                    }
                    break;
            }
#endif
        }
    }
}