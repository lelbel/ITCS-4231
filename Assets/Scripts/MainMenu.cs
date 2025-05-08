using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName;
    public string menuSceneName;
    public AudioMixer audioMixer;

    public void Play()
    {
        Debug.Log("Playing game...");
        SceneManager.LoadScene(gameSceneName);
    }

    public void Settings()
    {
        Debug.Log("Loading settings...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void LoadMenu() {
        SceneManager.LoadScene(menuSceneName);

    }
}