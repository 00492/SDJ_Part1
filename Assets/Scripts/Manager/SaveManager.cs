using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string RESSOURCE_NAME = "SaveManager";
    private const string SAVE_FILE = "SaveGame.json";

    public PlayerData _playerData;

    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            // Si je n'existe pas, je me crée
            if (_instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>(RESSOURCE_NAME);
                Instantiate(prefab);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Si j'existe déjà, je me détruit
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // USING JSON TEXT FILE--------------------------------------------------
    public void Save()
    {
        string path = GetPath(SAVE_FILE);

        EventManager.Instance.DispatchEvent(EventID.SaveGame);

        string json = JsonUtility.ToJson(_playerData);
        File.WriteAllText(path, json);

    }

    public void Load()
    {
        string path = GetPath(SAVE_FILE);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _playerData = JsonUtility.FromJson<PlayerData>(json);

            EventManager.Instance.DispatchEvent(EventID.LoadGame);
        }
    }

    private string GetPath(string path)
    {
        return Path.Combine(Application.persistentDataPath, path);
    }

    // USING PLAYERPREFS--------------------------------------------------
    public void SavePP(PlayerData data)
    {
        _playerData = data;

        PlayerPrefs.SetFloat("hp", data._hp);
    }

    public PlayerData LoadPP()
    {
        PlayerData data = new PlayerData();

        if (PlayerPrefs.HasKey("hp"))
        {
            data._hp = PlayerPrefs.GetFloat("hp");
        }

        return data;
    }
}
