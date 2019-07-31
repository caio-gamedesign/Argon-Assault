﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private void Start()
    {
        Invoke("LoadFirstLevel", 2f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
