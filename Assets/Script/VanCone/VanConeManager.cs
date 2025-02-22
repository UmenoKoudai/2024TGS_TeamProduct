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

    /// <summary>�P�̉����ɂ��Œ�10�b�̐�����݂���</summary>
    float _audioStopTime = 10f;

    float _timer = 0;

    /// <summary>�ߋ��ŐV�ŗ���������ID</summary>
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
        Scream1,  //�j���̔ߖ�
        Scream2,  //�����̔ߖ�
        bukiminawarai,  //�s�C���ȏ΂�
        oide,  //������
        tasukete, //������
        toiki, //�f��
        usotuki, //�R��

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
        ////�e�X�g�p
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
        //����������Ă�����
        if (_audio.isPlaying)
        {
            return;
        }

        if (_audio == null)
        {
            _audio = GetComponent<AudioSource>();
        }

        //���A���œ��������������̂�h��
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
