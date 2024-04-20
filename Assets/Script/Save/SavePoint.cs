using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    bool _isTrigger = false;

    private void Update()
    {
        if (_isTrigger)
        {
            //K�ŃZ�[�u
            if (Input.GetKeyDown(KeyCode.K))
            {
                //�Z�[�u����Ƃ��͂�����Ăׂ΂ł��܂�
                SaveLoadManager.Instance.SaveAction();
            }
            //L�Ń��[�h
            else if (Input.GetKeyDown(KeyCode.L))
            {
                //���[�h����Ƃ��͂�����Ăׂ΂ł��܂�
                SaveLoadManager.Instance.LoadAction();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isTrigger = false;
    }
}
