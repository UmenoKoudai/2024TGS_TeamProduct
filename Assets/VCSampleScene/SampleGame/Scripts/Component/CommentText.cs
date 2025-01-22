using UnityEngine;
using UnityEngine.UI;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// コメント
    /// </summary>
    public class CommentText : MonoBehaviour
    {
        [SerializeField] float _deathTime;
        [SerializeField] Text _text;

        float _timer = 0.0f;

        public void SetText(string text, int emotion)
        {
            _text.text = text;
            if (emotion > 20)
            {
                _text.color = Color.blue;
            }
            if (emotion < -20)
            {
                _text.color = Color.red;
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if(_timer > _deathTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}