using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VTNConnect
{
    /// <summary>
    /// インゲームの情報保存クラス
    /// NOTE: APIもここで投げる
    /// </summary>
    public class GameStateSave
    {
        public bool IsInGame => _gameHash != null;
        public string GameHash => _gameHash;

        string _gameHash = null;


#if AIGAME_IMPLEMENT
        APIGameStartAIGameImplement _gameStartAI = new APIGameStartAIGameImplement();
        APIGameEndAIGameImplement _gameEndAI = new APIGameEndAIGameImplement();

        List<UserDataResultSave> _saveData = new List<UserDataResultSave>();
        UserData[] _users = null;
#else
        APIGameStartImplement _gameStart = new APIGameStartImplement();
        APIGameEndImplement _gameEnd = new APIGameEndImplement();
#endif


#if AIGAME_IMPLEMENT
        public UserData[] Users => _users;

        GameEndAIGameRequest CreateAIGameResult()
        {
            var req = new GameEndAIGameRequest();
            req.GameHash = _gameHash;
            req.UserResults = GetUserSave();
            return req;
        }

        public async UniTask<VC_StatusCode> GameStartAIGame()
        {
            var result = await _gameStartAI.Request();
            var status = APIUtility.PacketCheck(result);
            if (status != VC_StatusCode.OK) return status;
            
            _gameHash = result.GameHash;
            _users = result.GameUsers;
            _saveData.Clear();

            return VC_StatusCode.OK;
        }

        public async UniTask<VC_StatusCode> GameEndAIGame()
        {
            var result = await _gameEndAI.Request(CreateAIGameResult());
            var status = APIUtility.PacketCheck(result);
            if (status != VC_StatusCode.OK) return status;
            
            _gameHash = null;
            _saveData.Clear();
            return VC_StatusCode.OK;
        }

        public UserDataResultSave[] GetUserSave()
        {
            if (_users.Length != _saveData.Count)
            {
                foreach (var u in _users)
                {
                    if (_saveData.Where(s => s.UserId == u.UserId).Count() > 0) continue;

                    _saveData.Add(new UserDataResultSave()
                    {
                        UserId = u.UserId,
                        GameResult = false,
                        MissionClear = false
                    });
                }
            }
            return _saveData.ToArray();
        }

        public void StackUser(int userId, bool gameResult, bool isMissionClear)
        {
            bool isFind = false;
            foreach (var u in _users)
            {
                if (u.UserId != userId) continue;

                _saveData.Add(new UserDataResultSave()
                {
                    UserId = userId,
                    GameResult = gameResult,
                    MissionClear = isMissionClear
                });
                isFind = true;
                break;
            }
            if (!isFind)
            {
                Debug.LogWarning("保存対象のユーザが見つかりませんでした");
            }
        }
#else
        public async UniTask<VC_StatusCode> GameStartVCGame(GameStartRequest req)
        {
            var result = await _gameStart.Request(req);
            var status = APIUtility.PacketCheck(result);
            if (status != VC_StatusCode.OK) return status;

            _gameHash = result.GameHash;
            return VC_StatusCode.OK;
        }

        public async UniTask<VC_StatusCode> GameEndVCGame(bool gameResult)
        {
            GameEndRequest request = new GameEndRequest();
            request.GameHash = _gameHash;
            request.GameResult = gameResult;

            var result = await _gameEnd.Request(request);
            var status = APIUtility.PacketCheck(result);
            if (status != VC_StatusCode.OK) return status;

            _gameHash = null;
            return VC_StatusCode.OK;
        }
#endif
    }
}