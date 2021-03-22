using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatsExample : MonoBehaviour
{
    public string _name;
    public float _x;
    public float _y;
    public float _hp;
    public int _kills;

    public Vector3 _pos;

    public void Save()
    {
        SaveManager.Instance.Save(GetCurrentData());
    }

    public void Load()
    {
        SetCurrentData(SaveManager.Instance.Load());
    }

    public void SavePP()
    {
        SaveManager.Instance.SavePP(GetCurrentData());
    }

    public void LoadPP()
    {
        SetCurrentData(SaveManager.Instance.LoadPP());
    }

    public SaveManager.PlayerData GetCurrentData()
    {
        SaveManager.PlayerData newData = new SaveManager.PlayerData();
        newData._name = _name;
        newData._posX = _x;
        newData._posY = _y;
        newData._hp = _hp;
        newData._killCount = _kills;
        newData._pos = _pos;
        return newData;
    }

    public void SetCurrentData(SaveManager.PlayerData data)
    {
        _name = data._name;
        _x = data._posX;
        _y = data._posY;
        _hp = data._hp;
        _kills = data._killCount;
        _pos = data._pos;
    }
}
