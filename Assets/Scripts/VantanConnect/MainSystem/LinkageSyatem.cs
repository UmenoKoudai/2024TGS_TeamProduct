using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

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
        UserData _user = null;
        VC_LoginView _view = null;

        public enum VC_LinkageEvent
        {
            Link = 1000,    //リンク
        }

        public void Setup(VC_LoginView view)
        {
            _view = view;
        }

        public void Reset()
        {
            //状態をリセット
            _user = null;
            SetViewEnable(true);

            //リセットをフックにデバッグ処理をコール
            //コネクト処理を常時行う
            if(VantanConnect.SystemSave.IsDebugConnect)
            {
                UniTask.RunOnThreadPool(async () =>
                {
                    var result = await _getUser.Request(VantanConnect.SystemSave.UseConnectUserId);
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
        }

        public void SetViewEnable(bool isEnableView)
        {
            _view?.SetEnable(isEnableView);
        }

        //チェインするデータを受け取り処理する
        public void OnEventCall(EventData data)
        {
            switch ((VC_LinkageEvent)data.EventId)
            {
                case VC_LinkageEvent.Link:
                    {
                        Debug.Log(JsonUtility.ToJson(data));
                        var gameId = data.GetIntData("GameId");
                        if (gameId != ProjectSettings.GameID) break;

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
            }
        }
    }
}