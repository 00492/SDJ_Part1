using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	private const string RESSOURCE_NAME = "EventManager";
	private Dictionary<EventID, Action<object>> _eventDict;

	private static EventManager _instance;
	public static EventManager Instance
	{
		get
		{
			// Si je n'existe pas, je me crée
			if (_instance == null)
			{
				GameObject prefab = Resources.Load<GameObject>(RESSOURCE_NAME);
				Instantiate(prefab);
			}
			return _instance;
		}
	}

	private void Awake()
	{
		// Si j'existe déjà, je me détruit
		if (_instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		_eventDict = new Dictionary<EventID, Action<object>>();
	}

	public void RegisterEvent(EventID id, Action<object> callback)
	{
		if (_eventDict.ContainsKey(id))
		{
			_eventDict[id] += callback;
		}
		else
		{
			_eventDict.Add(id, callback);
		}
	}

	public void UnregisterEvent(EventID id, Action<object> callback)
	{
		if (_eventDict.ContainsKey(id))
		{
			_eventDict[id] -= callback;
		}
		else
		{
			Debug.LogError("The event you are unregistering doesn't exist.");
		}
	}

	public void DispatchEvent(EventID id, object param = null)
	{
		if (_eventDict.ContainsKey(id))
		{
			_eventDict[id](param);
		}
		else
		{
			Debug.LogError("The event you are dispatching doesn't exist.");
		}
	}
}
