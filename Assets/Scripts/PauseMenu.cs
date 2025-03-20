using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public GameObject pauseMenuUI;


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused == true)
            {
                Resume();
            }

            else {
                Pause();
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
        SceneManager.LoadScene("Menu");

        //  deactivate UI
        pauseMenuUI.SetActive(false);

        //  start time
        Time.timeScale = 1f;

        isPaused = false;
    }

    public void QuitGame() {
        Application.Quit();
    }

    //----------HIDE CURSOR----------
    //  hide cursor if game window is focuse
    private void OnApplicationFocus(bool focus) {
        if (!isPaused) {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        else {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
}
