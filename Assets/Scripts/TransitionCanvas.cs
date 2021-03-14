using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TransitionCanvas : MonoBehaviour
{
    public const float TRANSITION_DURATION = 1f;
    private const string RESOURCE_NAME = "TransitionCanvas";
    public static Action<float> _onTransitionProgress;
    private static TransitionCanvas _instance;
    public static TransitionCanvas Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>(RESOURCE_NAME);
                GameObject go = Instantiate(prefab);
                DontDestroyOnLoad(go);
                _instance = go.GetComponent<TransitionCanvas>();
            }
            return _instance;
        }
    }
    private Image _blocker;
    private string _nextSceneName;
    private AsyncOperation _loadSceneOperation;
    void Awake()
    {
        _blocker = GetComponentInChildren<Image>();
    }
    public void ChangeScene(string sceneName, Action onSceneChanged = null)
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadSceneOperation.allowSceneActivation = false;
        if (onSceneChanged == null)
        {
            onSceneChanged = ChangeScene;
        }
        else
        {
            onSceneChanged += ChangeScene;
        }
        _nextSceneName = sceneName;
        StartCoroutine(FadeInRoutine(onSceneChanged));
    }
    private void ChangeScene()
    {
        _loadSceneOperation.allowSceneActivation = true;
    }
    private IEnumerator FadeInRoutine(System.Action onFadeDone)
    {
        _blocker.raycastTarget = true;
        for (float i = 0f; i < TRANSITION_DURATION; i += Time.deltaTime)
        {
            _blocker.color = Color.Lerp(Color.clear, Color.black, i);
            _onTransitionProgress?.Invoke(i);
            yield return null;
        }
        onFadeDone?.Invoke();
        for (float i = TRANSITION_DURATION; i > 0; i -= Time.deltaTime)
        {
            _blocker.color = Color.Lerp(Color.clear, Color.black, i);
            _onTransitionProgress?.Invoke(i);
            yield return null;
        }
        _blocker.raycastTarget = false;
    }
}