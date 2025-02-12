using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VTNConnect;

public class VanConeManager : MonoBehaviour, IVantanConnectEventReceiver
{
    [SerializeField]
    AudioClip[] _playSe;
    AudioSource _audio;

    [SerializeField]
    int _deathCount;
    public int DeathCount => _deathCount;

    public bool IsActive => true;

    private static VanConeManager _instance;
    public static VanConeManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new VanConeManager();

            return _instance;
        }
    }

    enum SeIndex
    {
        Scream1,  //男性の悲鳴
        Scream2,  //女性の悲鳴
        bukiminawarai,  //不気味な笑い
        oide,  //おいで
        tasukete, //助けて
        toiki, //吐息
        usotuki, //嘘つき

        Max,
    }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        VantanConnect.RegisterEventReceiver(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //テスト用
        if(Input.GetKeyDown(KeyCode.V))
        {
            CorpseSpown();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayScream();
        }
    }

    void CorpseSpown()
    {
        _deathCount++;
        Debug.Log(_deathCount);
    }

    void PlayScream()
    {
        int random = Random.Range(0, (int)SeIndex.Max);
        _audio.PlayOneShot(_playSe[random]);
    }

    public void OnEventCall(VTNConnect.EventData data)
    {
        switch (data.EventCode)
        {
            //case EventDefine.DeathStack:
            //    CorpseSpown();
            //    break;
            case EventDefine.DeathScream:
                PlayScream();
                break;
        }
    }
}
