﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    [SerializeField] private static bool pausedGame = false;
    [SerializeField] private string MenuName;
    [SerializeField] private string CurrentSceneName;
    public GameObject PauseMenu;
    public GameObject ControlsMenu;
    public GameObject GameOverMenu;
    public Image Hearts;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "intro_scene")
        {
            pausedGame = false;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
    }


    // Update is called once per frame
    void Update () {
        if (Hearts.sprite.name == "Hearts_5")
        {
            GameOver();
        }

		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
	}

    public void ResumeGame()
    {
        pausedGame = false;
        resetActives();
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        pausedGame = false;
        GameOverMenu.SetActive(false);    //replace GameOverMenuUI with StartUI
        PauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pausedGame = true;
        PauseMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Controls()
    {
        pausedGame = true;
        ControlsMenu.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        pausedGame = true;
        GameOverMenu.SetActive(true);
        PauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        pausedGame = false;
        Time.timeScale = 1f;
        resetActives();
        SceneManager.LoadScene(MenuName);
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void resetActives()
    {
        PauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
