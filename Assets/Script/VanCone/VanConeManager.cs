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

    [SerializeField]
    FlagData _flagData;

    /// <summary>１つの音声につき最長10秒の制限を設ける</summary>
    float _audioStopTime = 10f;

    float _timer = 0;

    /// <summary>過去最新で流した音声ID</summary>
    int _pastIndex = 7;

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
        ////テスト用
        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    _flagData.SetFlagStatus(true);
        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    PlayScream();
        //}

        if(_audio.isPlaying)
        {
            _timer += Time.deltaTime;
            if (_timer > _audioStopTime)
            {
                _audio.Stop();
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }
    }

    void CorpseSpown()
    {
        _deathCount++;
        Debug.Log(_deathCount);
    }

    void PlayScream()
    {
        //音声が流れていたら
        if (_audio.isPlaying)
        {
            return;
        }

        if (_audio == null)
        {
            _audio = GetComponent<AudioSource>();
        }

        //二回連続で同じ音声が流れるのを防ぐ
        int random = Random.Range(0, (int)SeIndex.Max);
        while (_pastIndex == random)
        {
            random = Random.Range(0, (int)SeIndex.Max);
        }
        _pastIndex = random;

        _audio.PlayOneShot(_playSe[random]);
    }

    public void OnEventCall(VTNConnect.EventData data)
    {

        switch (data.EventCode)
        {
            case EventDefine.KnockWindow:
                _flagData.SetFlagStatus(true);
                break;
            case EventDefine.DeathScream:
                PlayScream();
                break;
            case EventDefine.Cheer:
                CheerEvent cheer = new CheerEvent(data);
                if(cheer.GetEmotion() > 0)
                {

                }
                else if(cheer.GetEmotion() < 0)
                {

                }
                break;
        }
    }
}
