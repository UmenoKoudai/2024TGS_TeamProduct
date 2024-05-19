using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Tooltip("‘JˆÚ‚·‚éƒV[ƒ“‚Ì–¼‘O")]
    private string _nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            GameManager.Instance.BasePos = player.transform;
            SceneManager.LoadScene(_nextScene);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
