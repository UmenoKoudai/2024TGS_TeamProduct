using UnityEngine;
using NativeWebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WebSocketCli
{
    public delegate void WebSocketMessageCallback(byte[] msg);

    WebSocket _webSocket;
    WebSocketMessageCallback _callback;

    async public Task Connect(string uri, WebSocketMessageCallback callback, Dictionary<string,string> header = null)
    {
        _callback = callback;

        if (header != null)
        {
            _webSocket = new WebSocket(uri, header);
        }
        else
        {
            _webSocket = new WebSocket(uri);
        }

        _webSocket.OnOpen += () =>
        {
            Debug.Log("WebSocket Open");
        };

        _webSocket.OnError += (e) =>
        {
            Debug.Log("WebSocket Error Message: " + e);
        };

        _webSocket.OnClose += (e) =>
        {
            Debug.Log("WebSocket Close");
        };

        _webSocket.OnMessage += (bytes) =>
        {
            Debug.Log("WebSocket Message Data: " + System.Text.Encoding.UTF8.GetString(bytes));
            _callback?.Invoke(bytes);
        };

        // waiting for messages
        await _webSocket.Connect();
    }

    public bool IsClosed => _webSocket != null ? _webSocket.State == WebSocketState.Closed : true;

    public void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (_webSocket == null) return;
        _webSocket.DispatchMessageQueue();
#endif
    }

    async public void Send(string msg)
    {
        if (_webSocket == null) return;
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.SendText(msg);
        }
    }

    async public void Send<T>(T obj)
    {
        if (_webSocket == null) return;
        if (_webSocket.State == WebSocketState.Open)
        {
            string msg = JsonUtility.ToJson(obj);
            await _webSocket.SendText(msg);
        }
    }

    public async void Close()
    {
        if (_webSocket == null) return;

        await _webSocket.Close();
        _webSocket = null;
    }
}