using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string gameSceneName;
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Play() {
        Debug.Log("Playing game...");
        SceneManager.LoadScene(gameSceneName);
    }

    public void Settings() {
        Debug.Log("Loading settings...");
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
