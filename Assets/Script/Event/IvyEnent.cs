using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyEnent : MonoBehaviour,IEventObject
{
    public EventData EventData => _eventData;

    public EventTalkData ResultEventTalkData { get ; set; }

    [SerializeField] FlagList _flagList = null;
    [SerializeField] EventData _eventData = null;

    [SerializeField,Header("�ӂ�R�₷�����̐���")] private int _answerNumber;

    [SerializeField, Header("�ӂ�R�₹��͈͓���Player�����邩�ǂ���")] public bool _playerTrigger = true;

    public void ResultFlagCheck()
    {
        if (_eventData != null)
        {
            //���ׂ��t���O��true�̂Ƃ�
            if (_flagList.GetFlagStatus(_eventData.CheckFlag))
            {
                ResultEventTalkData = _eventData.TrueTalkData;
            }
            else //���ׂ��t���O��false�̂Ƃ�
            {
                ResultEventTalkData = _eventData.FalseTalkData;
                if (_eventData.ChangeFlag != null) _flagList.SetFlag(_eventData.ChangeFlag);
            }
        }
    }


    public void AnswerNumberCheck(int itemNum) //�ӂɑ΂��đI�����ꂽ�����Ă���A�C�e���������Ă邩�ǂ���
    {
        if(itemNum == _answerNumber)
        {
            Debug.Log("�ԍ������Ă܂�");
        }
        else
        {
            Debug.Log("���ɂ܂���");
            SceneChange sceneChange = FindObjectOfType<SceneChange>();
            sceneChange.ChangeScene("GameOver");
        }
    }

    //�v���C���[���ӂ͈͓̔��ɂ��邩�ǂ����ύX���邽�߂�Bool�ύX���Ă�
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
