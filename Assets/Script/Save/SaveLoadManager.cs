using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] SaveData _data;
    string _filePath;
    [SerializeField, Header("保存先のファイル名")] string _fileName;
    [SerializeField]
    Button[] _saveButton = new Button[10];
    [SerializeField]
    Button[] _loadButton = new Button[10];
    [SerializeField]
    GameObject _savePanel;
    [SerializeField]
    GameObject _loadPanel;

    SaveData[] _saveDate = new SaveData[10];
    

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

        if (!File.Exists(_filePath))
        {
            Save();
        }

        //タイトルなどで変化をつける場合最初にロードしておきたいため
        InitLoad();
    }

    public void OpenSavePanel()
    {
        _savePanel.SetActive(true);
    }
    public void CloseSavePanel()
    {
        _savePanel.SetActive(false);
    }
    public void OpenLoadPanel()
    {
        _loadPanel.SetActive(true);
    }
    public void CloseLoadPanel()
    {
        _loadPanel.SetActive(false);
    }

    //セーブしたいときに呼び出す
    public void SaveAction(int fileIndex)
    {
        _filePath = $"{Application.persistentDataPath}/SaveDate{fileIndex}.json";
        _data.Save();
        Save();
        _saveButton[fileIndex].GetComponentInChildren<Text>().text = _data.GetDateTime();
        _loadButton[fileIndex].GetComponentInChildren<Text>().text= _data.GetDateTime();
    }

    //ロードしたいときに呼び出す
    public void LoadAction(int filIndex)
    {
        _data = _saveDate[filIndex];
        _data.Load();
    }

    private void InitLoad()
    {
        for(int i = 0; i < 10; i++)
        {
            _filePath = $"{Application.persistentDataPath}/SaveDate{i}.json";
            if(File.Exists(_filePath))
            {
                Load();
                _saveDate[i] = _data;
                _saveButton[i].GetComponentInChildren<Text>().text = _data.GetDateTime();
            }
        }
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
}