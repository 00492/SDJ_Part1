using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;

    private void Awake()
    {
        _saveButton.onClick.AddListener(Save);
        _loadButton.onClick.AddListener(Load);
    }

    private void Start()
    {
        EventManager.Instance.RegisterEvent(EventID.UpdateHP, OnUpdateHP);
    }

    private void OnUpdateHP(object aArg)
    {
        _hpSlider.value = (float)aArg;
    }

    public void Save()
    {
        SaveManager.Instance.Save();
    }

    public void Load()
    {
        SaveManager.Instance.Load();
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnregisterEvent(EventID.UpdateHP, OnUpdateHP);
    }
}
