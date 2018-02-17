﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    [SerializeField] private static bool pausedGame = false;
    [SerializeField] private string MenuName;
    [SerializeField] private string CurrentSceneName;
    [SerializeField] private CameraMove MoveCam;
    public GameObject PauseMenu;
    public GameObject ControlsMenu;
    public GameObject GameOverMenu;

    // Update is called once per frame
    void Update () {
        if (MoveCam.playerOutOfRange())
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
        pausedGame = false;
        resetActives();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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