
using System;
using UnityEngine;

namespace VTNConnect
{

    enum WebSocketCommand
    {
        WELCOME = 1,
        JOIN = 2,
        EVENT = 3,
        GAMESTAT = 4,
        SEND_JOIN = 100,
        SEND_EVENT = 101,
        SEND_EPISODE = 102,
        SEND_USER_JOIN = 110,
    };

    [Serializable]
    public class WebSocketPacket
    {
        public string UserId;
        public int Command;
        public string Data;
    };

    [Serializable]
    public class WSPR_Welcome
    {
        public string SessionId;
    };

    [Serializable]
    public class VCActiveGame
    {
        public int GameId;
        public string Name;
        public int ActiveTime;
    }

    [Serializable]
    public class VCActiveUser
    {
        public int UserId;
        public string DisplayName;
        public int ActiveTime;
    }

    [Serializable]
    public class WSPR_GameStat
    {
        public VCActiveGame[] ActiveGames;
        public VCActiveUser[] ActiveUsers;
    };

    [Serializable]
    public class WSPS_SendEvent : EventData
    {
        public string SessionId;
        public int Command = (int)WebSocketCommand.SEND_EVENT;

        public WSPS_SendEvent(string sessionId, EventData d) : base(d)
        {
            SessionId = sessionId;
        }
    };

    [Serializable]
    public class WSPS_SendEpisode : GameEpisode
    {
        public string SessionId;
        public int Command = (int)WebSocketCommand.SEND_EPISODE;

        public WSPS_SendEpisode(string sessionId, GameEpisode d) : base(d)
        {
            SessionId = sessionId;
        }
    };

    [Serializable]
    public class WSPS_Join
    {
        public string SessionId;
        public int GameId;
        public string Version;
        public string BuildHash;
        public int Command = (int)WebSocketCommand.SEND_JOIN;
    };

}