using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public GameController gameController;

    public CameraShake cameraShake;
    public GameObject turret;
    public GameObject earth;
    public AudioSource button;
    public GameObject loseMenuUI;

    public int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = gameController.earthCurrentHealth;

        if(playerHealth <= 0)
        {
            StopGame();
        }
    }

    void StopGame()
    {
        loseMenuUI.SetActive(true);
        turret.GetComponent<TurretController>().enabled = false;
        turret.GetComponent<BulletScript>().enabled = false;
        earth.GetComponent<GameController>().enabled = false;
        cameraShake.StopAllCoroutines();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        button.Play();
        turret.GetComponent<TurretController>().enabled = true;
        turret.GetComponent<BulletScript>().enabled = true;
        earth.GetComponent<GameController>().enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameController.earthCurrentHealth = gameController.earthMaxHealth;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
