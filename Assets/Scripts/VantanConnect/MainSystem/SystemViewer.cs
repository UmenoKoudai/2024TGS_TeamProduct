using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VTNConnect
{
    /// <summary>
    /// インスペクタ情報表示用クラス
    /// </summary>
    public class SystemViewer : MonoBehaviour
    {
        [SerializeField] EventData _testData;

#if UNITY_EDITOR
        public EventData TestData => _testData;
#endif
    }
}