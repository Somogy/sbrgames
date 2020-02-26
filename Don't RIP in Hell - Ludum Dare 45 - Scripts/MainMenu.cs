﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void ExitBtn()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void StartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}
