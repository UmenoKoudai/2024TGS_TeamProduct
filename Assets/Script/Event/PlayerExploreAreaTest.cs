using UnityEngine;

public class PlayerExploreAreaTest : MonoBehaviour
{
    [SerializeField] EventObject _eventObj = null;
    [SerializeField] EventManager _eventManager = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_eventObj != null)
            {
                _eventManager.EventCheck(_eventObj);
                Debug.Log("調べた");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EventObject>(out _eventObj))
        {
            Debug.Log("近くに調べられるアイテムがあります");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EventObject>(out _eventObj))
        {
            _eventObj = null;
            Debug.Log("調べられるアイテムから遠ざかりました");
        }

    }
}
