using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static Network.WebRequest;
using Cysharp.Threading.Tasks;


namespace DataManagement
{
    /// <summary>
    /// マスターデータ管理クラス
    /// </summary>
    public partial class MasterData
    {
        //シングルトン運用
        static MasterData _instance = new MasterData();
        static public MasterData Instance => _instance;
        private MasterData() { }


        //読み込み管理
        public bool IsSetupComplete { get; private set; }
        delegate void LoadMasterDataCallback<T>(T data);
        static Dictionary<string, int> _versionInfos = null;


        static public string GetFileName(string sheetName)
        {
            return string.Format("{0}/{1}.json", DataPrefix, sheetName);
        }

        public async UniTask<int> Setup(Action onLoadCallback = null)
        {
            IsSetupComplete = false;

            Debug.Log("MasterData Load Start.");

            if (_versionInfos == null)
            {
                Debug.Log("StatusCheck Start.");

                var data = await LocalData.LoadAsync<MasterDataVersion>("MasterVersions");
                if (data == null || data?.TimeStamp < DateTime.Now.Ticks - 86400)
                {
                    _versionInfos = new Dictionary<string, int>();

                    string json = await GetRequest(GameSettings.MasterDataAPIURI);
                    MasterDataVersion dt = JsonUtility.FromJson<MasterDataVersion>(json);
                    await LocalData.SaveAsync<MasterDataVersion>(GetFileName("MasterVersions"), dt);
                }
            }

            await MasterDataLoad();

            IsSetupComplete = true;
            Debug.Log("MasterData Load Done.");
            onLoadCallback?.Invoke();

            return 0;
        }

        /// <summary>
        /// マスタデータ読み込み関数
        /// </summary>
        /// <typeparam name="T">マスタの型</typeparam>
        /// <param name="sheetName">シート名</param>
        public static async UniTask<T> LoadMasterData<T>(string sheetName) where T : SpreadSheetDataObject
        {
            var filename = GetFileName(sheetName);
            var data = await LocalData.LoadAsync<T>(filename);

            bool isUpdate = data == null;
            if (!isUpdate && _versionInfos.ContainsKey(sheetName))
            {
                Debug.Log($"Server:{_versionInfos[sheetName]} > Local:{data.Version}");
                isUpdate = _versionInfos[sheetName] > data.Version;
            }

            if (isUpdate)
            {
                string json = await GetRequest(string.Format("{0}?sheet={1}", GameSettings.MasterDataAPIURI, sheetName));
                Debug.Log(json);
                T dt = JsonUtility.FromJson<T>(json);
                await LocalData.SaveAsync<T>(filename, dt);
                Debug.Log("Network download. : " + filename + " / " + json + "/" + filename);
            }
            else
            {
                Debug.Log("Localfile used. : " + filename);
            }

            return data;
        }

        /// <summary>
        /// マスタデータ読み込み関数(コールバック受け取り)
        /// </summary>
        /// <typeparam name="T">マスタの型</typeparam>
        /// <param name="sheetName">シート名</param>
        public static async UniTask LoadMasterData<T>(string sheetName, Action<T> result) where T : SpreadSheetDataObject
        {
            result?.Invoke(await LoadMasterData<T>(sheetName));
        }
    }
}