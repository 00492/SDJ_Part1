using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject LevelManagerPrefab;

    private void Awake()
    {
        Instantiate(LevelManagerPrefab);
    }

    public void LoadMain()
    {
        LavelManager transition = LavelManager.Instance;

        transition.ChangeScene("FinishedParallax", transition.OpenInstructions);
    }
}
