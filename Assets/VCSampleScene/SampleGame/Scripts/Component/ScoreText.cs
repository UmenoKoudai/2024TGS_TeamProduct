using UnityEngine;
using UnityEngine.UI;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// スコア表示
    /// </summary>
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] Text _scoreText;

        private void Update()
        {
            _scoreText.text = ScoreManager.Score.ToString();
        }
    }
}