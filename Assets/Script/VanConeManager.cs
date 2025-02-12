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
        //�e�X�g�p
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
