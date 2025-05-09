using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public string menuSceneName;
    public AudioMixer audioMixer;

    void Update() {
        if (Input.GetButtonDown("Menu")) {
            if (isPaused == true) {
                Resume();
            }

            else {
                Pause();
            }
        }
    }

    public void Resume() {
        //  lock mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //  deavtivate UI
        pauseMenuUI.SetActive(false);

        //  start time
        Time.timeScale = 1f;

        isPaused = false;

    }

    void Pause() {
        // unlock mouse
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
