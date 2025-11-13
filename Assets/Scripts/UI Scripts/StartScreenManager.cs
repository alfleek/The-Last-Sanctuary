using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreenManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject ControlsPanel;

    void Start()
    {
        if (StartPanel != null) StartPanel.SetActive(true);
        if (ControlsPanel != null) ControlsPanel.SetActive(false);
    }

    public void HowTo()
    {
        if (StartPanel != null) StartPanel.SetActive(false);
        if (ControlsPanel != null) ControlsPanel.SetActive(true);
    }

    public void Play()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
