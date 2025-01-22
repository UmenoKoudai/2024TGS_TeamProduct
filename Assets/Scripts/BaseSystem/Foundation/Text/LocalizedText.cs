using UnityEngine;
using DataManagement;
using Cysharp.Threading.Tasks;
using TMPro;

/// <summary>
/// ローカライズ対応テキストクラス
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField, InspectorVariantName("TextMeshProの参照")] TextMeshProUGUI _text;
    [SerializeField, LocalizeText] string _textKey;

    private void Awake()
    {
        _text.text = "";
    }

    void Start()
    {
        UniTask.RunOnThreadPool(async () =>
        {
            await UniTask.WaitUntil(() => MasterData.Instance.IsSetupComplete) ;
                
            var text = MasterData.TextMaster[_textKey];
            if (text == null) return;

            _text.text = text;
        }).Forget();
    }

    public void SetString(string key)
    {
        _textKey = key;

        var text = MasterData.TextMaster[_textKey];
        if (text == null) return;

        _text.text = text;
    }

#if UNITY_EDITOR
    void OnEnable()
    {
        var text = MasterData.TextMaster[_textKey];
        if (text == null) return;

        _text.text = text;
    }
    void OnDisable()
    {
        var text = MasterData.TextMaster[_textKey];
        if (text == null) return;

        _text.text = text;
    }
#endif
}