using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public int earthMaxHealth;
    public int earthCurrentHealth;
    public float playerTime;
    public float playerHighTime;
    private string minutes;
    private string seconds;
    private string highMinutes;
    private string highSeconds;

    public CameraShake cameraShake;
    public HealthBar healthBar;

    public Text timeText;
    public Text highTimeText;

    // Start is called before the first frame update
    void Start()
    {
        earthCurrentHealth = earthMaxHealth;
        healthBar.SetMaxHealth(earthMaxHealth);
        playerTime = 0;

        playerHighTime = PlayerPrefs.GetFloat("HighTime");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        healthBar.setHealth(earthCurrentHealth);
        playerTime += Time.deltaTime;

        minutes = Mathf.Floor(playerTime/60).ToString("00");
        seconds = (playerTime%60).ToString("00");

        timeText.text = minutes + ":" + seconds;

        if(playerTime > playerHighTime)
        {
            playerHighTime = playerTime;
        }

        PlayerPrefs.SetFloat("HighTime", playerHighTime);

        highMinutes = Mathf.Floor(playerHighTime/60).ToString("00");
        highSeconds = (playerHighTime%60).ToString("00");

        highTimeText.text = "BEST TIME:\n" + highMinutes + ":" + highSeconds;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("LAsteroid"))
        {
            earthCurrentHealth -= 30;
            if(earthCurrentHealth > 0)
                StartCoroutine(cameraShake.Shake(.5f, .2f));

        }    
        else if(other.CompareTag("MAsteroid"))
        {
            earthCurrentHealth -= 20;
            if(earthCurrentHealth > 0)
                StartCoroutine(cameraShake.Shake(.5f, .2f));
        }
        else if(other.CompareTag("SAsteroid"))
        {
            earthCurrentHealth -= 10;
            if(earthCurrentHealth > 0)
                StartCoroutine(cameraShake.Shake(.5f, .2f));
        }
    }
}
