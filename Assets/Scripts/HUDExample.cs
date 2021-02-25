using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDExample : MonoBehaviour
{
    [SerializeField] private Slider _lifeBar;

    [SerializeField] private TextMeshProUGUI _killCount;
    [SerializeField] private TextMeshProUGUI _xpCount;
    private int _kills = 0;
    private int _xp = 0;


    private void Start()
    {
        Player._onKill += UpdateXP;
        Player._onPlayerHit += TakeDamage;
    }

    private void UpdateXP(int xp)
    {
        _kills++;
        _killCount.SetText("Kills : " + _kills.ToString());

        _xp += xp;
        _xpCount.SetText("XP : " + _xp.ToString());
    }


    public void TakeDamage()
    {
        _lifeBar.value -= 0.1f;
    }
}
