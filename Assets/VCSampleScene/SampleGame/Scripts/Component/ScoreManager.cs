using UnityEngine;
using UnityEngine.UI;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// スコア
    /// </summary>
    public class ScoreManager
    {
        static ScoreManager _instance = new ScoreManager();
        ScoreManager() { }

        int _score = 0;

        static public int Score => _instance._score;

        static public void AddScore(int add)
        {
            _instance._score += add;
        }
    }
}