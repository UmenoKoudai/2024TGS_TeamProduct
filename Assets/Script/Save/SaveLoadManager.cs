using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] SaveData _data;
    string _filePath;
    [SerializeField, Header("�ۑ���̃t�@�C����")] string _fileName;

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
        //�ۑ��擙
        //Debug.Log(Application.dataPath);
        //���݂������ɕۑ����Ă܂�
        //Debug.Log(Application.persistentDataPath);

        _data = GetComponent<SaveData>();
        _filePath = $"{Application.persistentDataPath}/{_fileName}.json";

        if (!File.Exists(_filePath))
        {
            Save();
        }

        //�^�C�g���Ȃǂŕω�������ꍇ�ŏ��Ƀ��[�h���Ă�����������
        LoadAction();
    }

    //�Z�[�u�������Ƃ��ɌĂяo��
    public void SaveAction()
    {
        _data.Save();
        Save();
    }

    //���[�h�������Ƃ��ɌĂяo��
    public void LoadAction()
    {
        Load();
        _data.Load();
    }

    void Save()
    {
        try
        {
            string json = JsonUtility.ToJson(_data);
            Debug.Log(json);
            using (StreamWriter wrter = new StreamWriter(_filePath, false))
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

    void Load()
    {
        try
        {
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string json = sr.ReadToEnd();
                Debug.Log(json);
                sr.Close();
                JsonUtility.FromJsonOverwrite(json, _data);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("���[�h���ɃG���[���������܂���: " + e.Message);
        }
    }

    public void FileNameChenge(string name)
    {
        _fileName = name;
        _filePath = $"{Application.persistentDataPath}/{_fileName}.json";

        if (!File.Exists(_filePath))
        {
            Save();
        }

        Debug.Log(_filePath);
    }
}