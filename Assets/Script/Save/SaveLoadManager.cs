using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] SaveData _data;
    string _filePath;
    [SerializeField, Header("保存先のファイル名")] string _fileName;

    #region　シングルトン

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
        //保存先等
        //Debug.Log(Application.dataPath);
        //現在こっちに保存してます
        //Debug.Log(Application.persistentDataPath);

        _data = GetComponent<SaveData>();
        _filePath = $"{Application.persistentDataPath}/{_fileName}.json";

        if (!File.Exists(_filePath))
        {
            Save();
        }

        //タイトルなどで変化をつける場合最初にロードしておきたいため
        LoadAction();
    }

    //セーブしたいときに呼び出す
    public void SaveAction()
    {
        _data.Save();
        Save();
    }

    //ロードしたいときに呼び出す
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
            Debug.LogError("セーブ中にエラーが発生しました: " + e.Message);
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
            Debug.LogError("ロード中にエラーが発生しました: " + e.Message);
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