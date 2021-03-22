using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public struct PlayerData
    {
        public string _name;
        public float _posX;
        public float _posY;
        public float _hp;
        public int _killCount;

        public Vector3 _pos;
    }

    private const string RESSOURCE_NAME = "SaveManager";

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
    public void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);

        File.WriteAllText("saveGame.json", json);
    }

    public PlayerData Load()
    {
        string json = File.ReadAllText("saveGame.json");

        return JsonUtility.FromJson<PlayerData>(json);
    }

    // USING PLAYERPREFS--------------------------------------------------
    public void SavePP(PlayerData data)
    {
        PlayerPrefs.SetFloat("posX", data._posX);
        PlayerPrefs.SetFloat("posY", data._posY);
        PlayerPrefs.SetFloat("hp", data._hp);
        PlayerPrefs.SetInt("kills", data._killCount);
        PlayerPrefs.SetString("name", data._name);
    }

    public PlayerData LoadPP()
    {
        PlayerData data = new PlayerData();

        if (PlayerPrefs.HasKey("posX"))
        {
            data._posX = PlayerPrefs.GetFloat("posX");
        }
        if (PlayerPrefs.HasKey("posY"))
        {
            data._posY = PlayerPrefs.GetFloat("posY");
        }
        if (PlayerPrefs.HasKey("hp"))
        {
            data._hp = PlayerPrefs.GetFloat("hp");
        }
        if (PlayerPrefs.HasKey("kills"))
        {
            data._killCount = PlayerPrefs.GetInt("kills");
        }
        if (PlayerPrefs.HasKey("name"))
        {
            data._name = PlayerPrefs.GetString("name");
        }

        return data;
    }
}
