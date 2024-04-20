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
            //Kでセーブ
            if (Input.GetKeyDown(KeyCode.K))
            {
                //セーブするときはこれを呼べばできます
                SaveLoadManager.Instance.SaveAction();
            }
            //Lでロード
            else if (Input.GetKeyDown(KeyCode.L))
            {
                //ロードするときはこれを呼べばできます
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
