using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _targetPos;

    private float _moveTime = 1f;
    private bool _goingUp = true;

    private float _hp = 100;

    private void Start()
    {
        _startPos = transform.position;
        _targetPos = _startPos + new Vector2(0, 3);

        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        float timer = 0f;
        while (timer < _moveTime)
        {
            timer += Time.deltaTime;

            float lerpValue = timer / _moveTime;

            if (_goingUp)
            {
                transform.position = Vector2.Lerp(_startPos, _targetPos, lerpValue);
            }
            else
            {
                transform.position = Vector2.Lerp(_targetPos, _startPos, lerpValue);
            }
            yield return new WaitForEndOfFrame();
        }

        _goingUp = !_goingUp;
        StartCoroutine(PatrolRoutine());
    }

    public void TakeDamage(float dmg)
    {
        _hp -= dmg;

        if (_hp <= 0)
        {
            Player._onKill?.Invoke(10);
            Destroy(gameObject);
        }
    }
}
