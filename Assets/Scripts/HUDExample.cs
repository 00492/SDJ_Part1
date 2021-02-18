using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDExample : MonoBehaviour
{
    [SerializeField] private Slider _lifeBar;

    [SerializeField] private TextMeshProUGUI _timerText;
    private float _realTimer = 20f;


    private void Start()
    {
        StartCoroutine(TickTock());
    }

    private IEnumerator TickTock()
    {
        while (_realTimer > 0)
        {
            _realTimer -= Time.deltaTime;
            _timerText.SetText(_realTimer.ToString("0"));
            yield return null;
        }
    }


    public void TakeDamage()
    {
        _lifeBar.value -= 0.1f;
    }
}
