using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Tooltip("‘JˆÚ‚·‚éƒV[ƒ“‚Ì–¼‘O")]
    private string _nextScene;
    [SerializeField]
    private string _posName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            Debug.Log("ChangeScene");
            GameManager.Instance.PosName = _posName;
            GameManager.Instance.Direction = player.transform.up;
            GameManager.instance.StateChange(GameManager.SystemState.SceneMove);
            SceneManager.LoadScene(_nextScene);
           
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
