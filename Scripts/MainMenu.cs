using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource button;
    public void startGame()
    {
        button.Play();
        SceneManager.LoadScene("Intro");
    }

    public void quitGame()
    {
        button.Play();
        Application.Quit();
    }
}
