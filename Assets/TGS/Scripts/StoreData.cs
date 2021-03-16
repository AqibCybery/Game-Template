using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Security;
using System.Text;
using System.Security.Cryptography;
using System;
using Newtonsoft.Json;
public class StoreData : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    public static Save CreateSaveGameObject()
    {
        Save save = new Save
        {
            Score = 300,
            UnlockLevels = 3,
            TotalLevels = 10,
            UnlockGuns = 2,
            RemoveAds = 0,
        };
        return save;
    }
    public static void CreateSaveGameObject(Save DataforSave)
    {
        Debug.Log(JsonUtility.ToJson(DataforSave));
        UIManager.Instance.Score.text = DataforSave.Score.ToString();
        string json = JsonUtility.ToJson(DataforSave);
        json = Encrypt(json);
        if(!File.Exists(Application.persistentDataPath + "/gamesave.json"))
        File.Create(Application.persistentDataPath + "/gamesave.json");
        File.WriteAllText(Application.persistentDataPath + "/gamesave.json", json);

    }
    public static string Encrypt(string clearText)
    {
        string EncryptionKey = "abc123";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "abc123";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    public static Save SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);
        json = Encrypt(json);
        if (!File.Exists(Application.persistentDataPath + "/gamesave.json"))
            File.Create(Application.persistentDataPath + "/gamesave.json");
        File.WriteAllText(Application.persistentDataPath + "/gamesave.json", json);
       return JsonConvert.DeserializeObject<Save>(Decrypt(File.ReadAllText(Application.persistentDataPath + "/gamesave.json")));
    }
    public static Save LoadAsObj()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.json")&& !string.IsNullOrEmpty(File.ReadAllText(Application.persistentDataPath + "/gamesave.json").ToString()))
            return JsonConvert.DeserializeObject<Save>(Decrypt(File.ReadAllText(Application.persistentDataPath + "/gamesave.json")));
        else
        {           
            return SaveAsJSON();
        }

    } 
    public static string LoadAsJSON()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.json") && !string.IsNullOrEmpty(File.ReadAllText(Application.persistentDataPath + "/gamesave.json").ToString()))
            return Decrypt(File.ReadAllText(Application.persistentDataPath + "/gamesave.json"));
        else
            return string.Empty;
    }
}
[System.Serializable]
public class Save
{
    public int Score = 0;
    public int UnlockLevels = 0;
    public int TotalLevels = 10;
    public int UnlockGuns = 0;
    public int RemoveAds = 0;
}