using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using GameLoopTest;
using UnityEngine.SceneManagement;

namespace VTNConnect
{
    /// <summary>
    /// 簡単なインゲーム
    /// </summary>
    public class InGame : MonoBehaviour, IVantanConnectEventReceiver
    {
        [SerializeField] float _spawnInterval;
        [SerializeField] GameObject _player;
        [SerializeField] GameObject _enemy;
        [SerializeField] GameObject _comment;
        [SerializeField] GameObject _canvasRoot;

        float _timer = 0.0f;

        void Awake()
        {
            //イベントを受け取るためには登録が必要
            VantanConnect.RegisterEventReceiver(this);
        }

        public bool IsActive => true;

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _spawnInterval)
            {
                _timer -= _spawnInterval;
                GameObject.Instantiate(_enemy, new Vector3(UnityEngine.Random.Range(-20,20), 50, -1), Quaternion.identity);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
#if AIGAME_IMPLEMENT
                //ゲーム終了
                VantanConnect.GameEnd((VC_StatusCode status) =>
                {
                    SceneManager.LoadScene("Title");
                });
#else
                //ゲーム終了
                VantanConnect.GameEnd(false, (VC_StatusCode status) =>
                {
                    SceneManager.LoadScene("Title");
                });
#endif
            }

            if(_timer > 10.0f)
            {
#if AIGAME_IMPLEMENT
                //ゲーム終了
                VantanConnect.GameEnd((VC_StatusCode status) =>
                {
                    SceneManager.LoadScene("Title");
                });
#else
                //ゲーム終了
                VantanConnect.GameEnd(true, (VC_StatusCode status) =>
                {
                    SceneManager.LoadScene("Title");
                });
#endif
            }
        }

        public void OnEventCall(EventData data)
        {
            switch(data.EventCode)
            {
                //おうえんイベント
                case EventDefine.Cheer:
                    {
                        //扱いやすいようクラス変換
                        CheerEvent cheer = new CheerEvent(data);

                        //コメントオブジェクト生成
                        var go = GameObject.Instantiate(_comment, _canvasRoot.transform);
                        var script = go.GetComponent<CommentText>();
                        var transform = go.GetComponent<RectTransform>();
                        transform.position = new Vector3(UnityEngine.Random.Range(250, 1550), UnityEngine.Random.Range(-250, 550));
                        script.SetText(cheer.GetMessage(), cheer.GetEmotion());

                        //応援の感情値によって、サイズが変わる
                        if(cheer.GetEmotion() > 0)
                        {
                            _player.transform.localScale += new Vector3(5,0,0);
                        }
                        if (cheer.GetEmotion() < 0)
                        {
                            _player.transform.localScale -= new Vector3(5, 0, 0);
                            if(_player.transform.localScale.y < 10)
                            {
                                _player.transform.localScale = new Vector3(10, 1, 1);
                            }
                        }
                    }
                    break;
            }
        }
    }
}