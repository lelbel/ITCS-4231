using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public string menuSceneName;


    void Update() {
        if (Input.GetButtonDown("Menu")) {
            if (isPaused == true)
            {
                Resume();
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
            }

            else {
                Pause();
                //Cursor.visible = true;
            }
        }
    }

    public void Resume() {
        //  deavtivate UI
        pauseMenuUI.SetActive(false);

        //  start time
        Time.timeScale = 1f;

        isPaused = false;

    }

    void Pause() {
        //  activate UI
        pauseMenuUI.SetActive(true);

        //  stop time
        Time.timeScale = 0f;

        isPaused = true;
    }

    public void LoadMenu() {
        SceneManager.LoadScene(menuSceneName);

        //  deactivate UI
        pauseMenuUI.SetActive(false);

        //  start time
        Time.timeScale = 1f;

        isPaused = false;
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
