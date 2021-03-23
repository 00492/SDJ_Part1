using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomePlayer : MonoBehaviour
{
    public float _currentHP;
    public float _maxHP;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    private void Start()
    {
        EventManager.Instance.RegisterEvent(EventID.SaveGame, OnSaveGame);
        EventManager.Instance.RegisterEvent(EventID.LoadGame, OnLoadGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentHP -= 10;
            UpdateHp();
        }
    }

    private void UpdateHp()
    {
        float hpPercent = 0f;
        if (_maxHP != 0)
        {
            hpPercent = _currentHP / _maxHP;

            Debug.Log(hpPercent);
        }
        EventManager.Instance.DispatchEvent(EventID.UpdateHP, hpPercent);
    }
    private void OnLoadGame(object aArg)
    {
        _currentHP = SaveManager.Instance._playerData._hp;

        UpdateHp();
    }

    private void OnSaveGame(object aArg)
    {
        SaveManager.Instance._playerData._hp = _currentHP;
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnregisterEvent(EventID.SaveGame, OnSaveGame);
        EventManager.Instance.UnregisterEvent(EventID.LoadGame, OnLoadGame);
    }
}
