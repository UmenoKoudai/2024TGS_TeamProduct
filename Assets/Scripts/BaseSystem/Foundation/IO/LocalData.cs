using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cysharp.Threading.Tasks;

public class LocalData
{
    static string BasePath => Application.dataPath + "/StreamingAssets"; //Application.persistentDataPath; //デバッグしやすくした。秘匿したいデータはpersistentDataPathを使う事。

    /// <summary>
    /// ファイルを読み込みます
    /// </summary>
    /// <typeparam name="T">クラスの型</typeparam>
    /// <param name="file">ファイル名を指定</param>
    /// <param name="path">保存先を変えたいときに指定</param>
    /// <param name="fenc">暗号化を強制的に書けたいときに指定</param>
    /// <returns></returns>
    static public T Load<T>(string file, string path = null, bool fenc = false)
    {
        try
        {
            if (path == null)
            {
                path = BasePath;
            }

            //ファイルがなかったらnullで返す
            if (!File.Exists(path + "/" + file))
            {
                return default(T);
            }

            var arr = File.ReadAllBytes(path + "/" + file);
#if RELEASE
            arr = AesDecrypt(arr);
#else
            if (fenc)
            {
                arr = AesDecrypt(arr);
            }
#endif
            string json = Encoding.UTF8.GetString(arr);
            return JsonUtility.FromJson<T>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Load{file}");
            Debug.LogError(ex.Message);
        }
        return default(T);
    }

    static public async UniTask<T> LoadAsync<T>(string file, string path = null, bool fenc = false)
    {
        try
        {
            if (path == null)
            {
                path = BasePath;
            }

            //ファイルがなかったらnullで返す
            if (!File.Exists(path + "/" + file))
            {
                return default(T);
            }

            string json = "";

            using (FileStream fs = new FileStream(path + "/" + file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var bytes = new byte[fs.Length];
                await fs.ReadAsync(bytes, 0, bytes.Length);

#if RELEASE
        fenc = true;
#endif
                if (fenc)
                {
                    bytes = AesDecrypt(bytes);
                }
                json = Encoding.UTF8.GetString(bytes);
            }

            return JsonUtility.FromJson<T>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"LoadAsync{file}"); 
            Debug.LogError(ex.Message);
        }
        return default(T);
    }

    /// <summary>
    /// ファイルを保存します
    /// </summary>
    /// <typeparam name="T">クラスの型</typeparam>
    /// <param name="file">ファイル名</param>
    /// <param name="data">セーブするデータ</param>
    /// <param name="path">保存先を変えたいときに指定</param>
    /// <param name="fenc">暗号化を強制的に書けたいときに指定</param>
    /// <returns></returns>
    static public void Save<T>(string file, T data, string path = null, bool fenc = false)
    {
        try
        {
            if (path == null)
            {
                path = BasePath;
            }

            var json = JsonUtility.ToJson(data);
            byte[] arr = Encoding.UTF8.GetBytes(json);
#if RELEASE
            arr = AesEncrypt(arr);
#else
            if (fenc)
            {
                arr = AesEncrypt(arr);
            }
#endif
            var pathes = (path + "/" + file).Split('/').ToList();
            pathes.RemoveAt(pathes.Count - 1);
            var dir = string.Join("/", pathes);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(path + "/" + file, arr);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Save{file}");
            Debug.LogError(ex.Message);
        }
    }

    static public async UniTask SaveAsync<T>(string file, T data, string path = null, bool fenc = false)
    {
        try
        {
            if (path == null)
            {
                path = BasePath;
            }

            var json = JsonUtility.ToJson(data);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            using (FileStream fs = new FileStream(path + "/" + file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
#if RELEASE
        fenc = true;
#endif
                if (fenc)
                {
                    bytes = AesEncrypt(bytes);
                }

                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"SaveAsync{file}");
            Debug.LogError(ex.Message);
        }
    }

    /// <summary>
    /// ファイルを消します
    /// </summary>
    /// <typeparam name="T">クラスの型</typeparam>
    /// <param name="file">ファイル名を指定</param>
    /// <param name="path">保存先を変えたいときに指定</param>
    /// <returns></returns>
    static public void Delete(string file, string path = null)
    {
        if (path == null)
        {
            path = Application.persistentDataPath;
        }

        //ファイルがなかったらnullで返す
        if (!File.Exists(path + "/" + file))
        {
            return;
        }

        File.Delete(path + "/" + file);
    }

    /// <summary>
    /// AES暗号化
    /// </summary>
    static public byte[] AesEncrypt(byte[] byteText)
    {
        // AES設定値
        //===================================
        int aesKeySize = 128;
        int aesBlockSize = 128;
        string aesIv = "cCYcP6Is7Y6EpGN9";
        string aesKey = "ieSeuIx0ZF4s8s1M";
        //===================================

        // AESマネージャー取得
        var aes = GetAesManager(aesKeySize, aesBlockSize, aesIv, aesKey);
        // 暗号化
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES復号化
    /// </summary>
    static public byte[] AesDecrypt(byte[] byteText)
    {
        // AES設定値
        //===================================
        int aesKeySize = 128;
        int aesBlockSize = 128;
        string aesIv = "cCYcP6Is7Y6EpGN9";
        string aesKey = "ieSeuIx0ZF4s8s1M";
        //===================================

        // AESマネージャー取得
        var aes = GetAesManager(aesKeySize, aesBlockSize, aesIv, aesKey);
        // 復号化
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    /// <summary>
    /// AesManagedを取得
    /// </summary>
    /// <param name="keySize">暗号化鍵の長さ</param>
    /// <param name="blockSize">ブロックサイズ</param>
    /// <param name="iv">初期化ベクトル(半角X文字（8bit * X = [keySize]bit))</param>
    /// <param name="key">暗号化鍵 (半X文字（8bit * X文字 = [keySize]bit）)</param>
    static private AesManaged GetAesManager(int keySize, int blockSize, string iv, string key)
    {
        AesManaged aes = new AesManaged();
        aes.KeySize = keySize;
        aes.BlockSize = blockSize;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(iv);
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }


    /// <summary>
    /// XOR
    /// </summary>
    static public byte[] Xor(byte[] a, byte[] b)
    {
        int j = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (j < b.Length)
            {
                j++;
            }
            else
            {
                j = 1;
            }
            a[i] = (byte)(a[i] ^ b[j - 1]);
        }
        return a;
    }
}
