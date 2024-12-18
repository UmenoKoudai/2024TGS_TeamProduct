using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class SaveLoadManager : MonoBehaviour
{
    //[SerializeField] SaveData _data;
    string _filePath;
    [SerializeField, Header("�ۑ���̃t�@�C����")] string _fileName;
    [SerializeField]
    Button[] _saveButton = new Button[10];
    [SerializeField]
    Button[] _loadButton = new Button[10];
    [SerializeField]
    GameObject _savePanel;
    [SerializeField]
    GameObject _loadPanel;

    Date[] _saveDate = new Date[10];

    [Header("�Z�[�u����l�̊m�F")]
    Transform _savePos;
    bool[] _flags;
    string _nowTime = "";
    Date _date;

    public class Date
    {

        public float X;
        public float Y;
        public float Z;
        public string NowTime;
        public bool[] Flag;
    }


    #region�@�V���O���g��

    public static SaveLoadManager instance;
    public static SaveLoadManager Instance
    {
        get
        {
            if (!instance)
            {
                SetupInstance();
            }

            return instance;
        }
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    static void SetupInstance()
    {
        instance = FindObjectOfType<SaveLoadManager>();

        if (!instance)
        {
            GameObject go = new GameObject();
            instance = go.AddComponent<SaveLoadManager>();
            go.name = instance.GetType().Name;
            DontDestroyOnLoad(go);
        }
    }
    #endregion

    void Start()
    {
        _date = new Date();
        //�ۑ��擙
        //Debug.Log(Application.dataPath);
        //���݂������ɕۑ����Ă܂�
        //Debug.Log(Application.persistentDataPath);
        if (!File.Exists(_filePath))
        {
          //Save();
        }
        for(int i = 0; i < _saveDate.Length; i++)
        {
            _saveDate[i] = new Date();
        }
        //�^�C�g���Ȃǂŕω�������ꍇ�ŏ��Ƀ��[�h���Ă�����������
        InitLoad();
    }

    public void OpenSavePanel()
    {
        _savePanel.SetActive(true);
        //TimeReflection(_saveButton);
    }
    public void CloseSavePanel()
    {
        _savePanel.SetActive(false);
    }
    public void OpenLoadPanel()
    {
        _loadPanel.SetActive(true);
        //TimeReflection(_loadButton);
    }
    public void CloseLoadPanel()
    {
        _loadPanel.SetActive(false);
    }

    private void TimeReflection(Button[] buttons)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            var saveTime = _saveDate[i].NowTime;
            if (saveTime != "")
            {
                buttons[i].GetComponentInChildren<Text>().text = saveTime.ToString();
            }
            else
            {
                return;
            }
        }
    }

    //�Z�[�u�������Ƃ��ɌĂяo��
    public void SaveAction(int fileIndex)
    {
        _filePath = $"{Application.persistentDataPath}/SaveDate{fileIndex}.json";
        //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
        _savePos = FindObjectOfType<Player>().transform;

        var dateTime = DateTime.Now;
        _nowTime = $"{dateTime.Year}/{dateTime.Month}/{dateTime.Day} {dateTime.Hour}:{dateTime.Minute}";

        //�t���O����
        _flags = new bool[GameManager.Instance.FlagList.Flags.Count];
        for (int i = 0; i < GameManager.Instance.FlagList.Flags.Count; i++)
        {
            _flags[i] = GameManager.Instance.FlagList.GetFlagStatus(GameManager.Instance.FlagList.Flags[i]);

        }
        _date.X = _savePos.position.x;
        _date.Y = _savePos.position.y;
        _date.Z = _savePos.position.z;
        _date.NowTime = _nowTime;
        _date.Flag = _flags;
        _saveDate[fileIndex] = _date;
        Save(_filePath);
        _saveButton[fileIndex].GetComponentInChildren<Text>().text = _nowTime;
        _loadButton[fileIndex].GetComponentInChildren<Text>().text= _nowTime;
    }

    //���[�h�������Ƃ��ɌĂяo��
    public void LoadAction(int filIndex)
    {
        var loadDate = _saveDate[filIndex];
        _savePos.position = new Vector3(loadDate.X, loadDate.Y, loadDate.Z);
        _nowTime = loadDate.NowTime;
        _flags = loadDate.Flag;
        try
        {
            //�Z�[�u�n�_�A�L�����N�^�[�̈ʒu
            FindObjectOfType<Player>().transform.position = _savePos.position;
            //san�l����
            //
            //�t���O����
            for (int i = 0; i < _flags.Length; i++)
            {
                GameManager.Instance.FlagList.SetFlag(GameManager.Instance.FlagList.Flags[i], _flags[i]);
            }
        }
        catch
        {
            Debug.LogError("�f�[�^�����݂��܂���");
        }

    }

    private void InitLoad()
    {
        for(int i = 0; i < 10; i++)
        {
            _filePath = $"{Application.persistentDataPath}/SaveDate{i}.json";
            if(File.Exists(_filePath))
            {
                _saveDate[i] = Load(_filePath);
                _saveButton[i].GetComponentInChildren<Text>().text = _saveDate[i].NowTime;
            }
        }
    }

    //�Z�[�u����Ƃ��ɒl��������
    public void Save(string filePath)
    {
        try
        {
            string json = JsonUtility.ToJson(_date);
            Debug.Log(json);
            using (StreamWriter wrter = new StreamWriter(filePath, false))
            {
                wrter.WriteLine(json);
                wrter.Close();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("�Z�[�u���ɃG���[���������܂���: " + e.Message);
        }
    }

    //���[�h����Ƃ��ɒl��������
    public Date Load(string failePath)
    {
        Date curentDate = new Date();
        try
        {
            using (StreamReader sr = new StreamReader(failePath))
            {
                string json = sr.ReadToEnd();
                Debug.Log(json);
                sr.Close();
                curentDate = JsonUtility.FromJson<Date>(json);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("���[�h���ɃG���[���������܂���: " + e.Message);
        }
        return curentDate;
    }
}