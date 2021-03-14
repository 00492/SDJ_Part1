using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LavelManager : MonoBehaviour
{
    public const float TRANSITION_DURATION = 1f;
    public static Action<float> _onTransitionProgress;

    [SerializeField] private Image _blocker;
    [SerializeField] private GameObject _instructions;

    private string _nextSceneName;
	private AsyncOperation _loadSceneOperation;

	private static LavelManager m_Instance;
	public static LavelManager Instance
	{
		get { return m_Instance; }
	}

	private void Awake()
	{
		if (m_Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			m_Instance = this;
			DontDestroyOnLoad(gameObject);
		}
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

    public void OpenInstructions()
    {
        _instructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        _instructions.SetActive(false);
    }
}
