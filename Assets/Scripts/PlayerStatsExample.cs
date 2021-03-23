using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatsExample : MonoBehaviour
{
    public float _hp;

    public Vector3 _pos;

    public void Save()
    {
        //SaveManager.Instance.SavePlayer(GetCurrentData());
    }

    public void Load()
    {
        //SetCurrentData(SaveManager.Instance.LoadPlayer());
    }

    public void SavePP()
    {
        SaveManager.Instance.SavePP(GetCurrentData());
    }

    public void LoadPP()
    {
        SetCurrentData(SaveManager.Instance.LoadPP());
    }

    public PlayerData GetCurrentData()
    {
        PlayerData newData = new PlayerData();
        newData._hp = _hp;
        newData._pos = _pos;
        return newData;
    }

    public void SetCurrentData(PlayerData data)
    {
        _hp = data._hp;
        _pos = data._pos;
    }
}
