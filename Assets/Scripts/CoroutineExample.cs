using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{

    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;

    [SerializeField] private float _moveTime = 2f;

    void Start()
    {
        StartCoroutine(WaitSomeTime());

        StartCoroutine(TimerWait());

        StartCoroutine(LerpPosition());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //StopCoroutine(LerpPosition());
            StopAllCoroutines();
            StartCoroutine(LerpPosition());
        }
    }

    private IEnumerator WaitSomeTime()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForEndOfFrame();
        yield return null;
        Debug.Log("1 frame has passed");


        yield return new WaitForSeconds(1f);
        Debug.Log("1 second has passed");


        yield return new WaitForSeconds(3f);
        Debug.Log("Total of 4 seconds");
    }

    private IEnumerator TimerWait()
    {
        float timer = 0f;
        while(timer < 5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Debug.Log("timer of 5 seconds ended");
    }

    private IEnumerator LerpPosition()
    {
        float timer = 0f;

        while(timer < _moveTime)
        {
            timer += Time.deltaTime;

            float lerpValue = timer / _moveTime; // This will be between 0 and 1
            transform.position = Vector3.Lerp(_startPos.position, _endPos.position, lerpValue);

            yield return new WaitForEndOfFrame();
        }
    }
}
