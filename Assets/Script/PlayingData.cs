using UnityEngine;

public class PlayingData : MonoBehaviour
{

    private static PlayingData _instance;
    public static PlayingData Instance
    {
        get
        {
            if(_instance is null)
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
        if(FindObjectsOfType<PlayingData>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public string PosName { get; set; }
    public Vector3 Direction { get; set; }
}
