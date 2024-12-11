using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyEnent : MonoBehaviour,IEventObject
{
    public EventData EventData => _eventData;

    public EventTalkData ResultEventTalkData { get ; set; }

    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;

    [SerializeField,Header("蔦を燃やす正解の数字")] private int _answerNumber;

    [SerializeField, Header("蔦を燃やせる範囲内にPlayerがいるかどうか")] public bool _playerTrigger = true;

    public void ResultFlagCheck()
    {
        if (_eventData != null)
        {
            //調べたフラグがtrueのとき
            if (_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                ResultEventTalkData = _eventData.TrueTalkData;
            }
            else //調べたフラグがfalseのとき
            {
                ResultEventTalkData = _eventData.FalseTalkData;
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }
        }
    }


    public void AnswerNumberCheck(int itemNum) //蔦に対して選択された持っているアイテムがあってるかどうか
    {
        if(itemNum == _answerNumber)
        {
            Debug.Log("番号合ってます");
        }
        else
        {
            Debug.Log("死にました");
            SceneChange sceneChange = FindObjectOfType<SceneChange>();
            sceneChange.ChangeScene("GameOver");
        }
    }

    //プレイヤーが蔦の範囲内にいるかどうか変更するためにBool変更してる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player playerScript;
        if(collision.gameObject.TryGetComponent<Player>(out playerScript))
        {
            _playerTrigger = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Player playerScript;
        if (collision.gameObject.TryGetComponent<Player>(out playerScript))
        {
            _playerTrigger = true;
        }
    }
}
