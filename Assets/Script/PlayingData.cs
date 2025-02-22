using UnityEngine;

public class PlayingData : MonoBehaviour
{

    private static PlayingData _instance;
    public static PlayingData Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayingData>();
                if(_instance == null)
                {
                    Debug.LogError("PlayingDataÇ™ë∂ç›ÇµÇ‹ÇπÇÒ");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string PosName { get; set; }
    public Vector3 Direction { get; set; }
}
