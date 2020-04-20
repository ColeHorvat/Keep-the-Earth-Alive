using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawner : MonoBehaviour
{
    private int randomChance;

    public Transform spawnPoint;
    public GameObject[] enemies;

    private Rigidbody2D rb;
    private GameObject enemy;

    public float asteroidSpeed = -3f;
    public float spawnTime;

    public float spawnMin;
    public float spawnMax;
    private Vector2 pos1;
    private Vector2 pos2;
    private float speed = 1.0f; 

    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;

        spawnMin = 4;
        spawnMax = 8;

        pos1 = transform.position;
        pos2 = new Vector2(transform.position.x - 1, transform.position.y);
    }

    void Update()
    {

        transform.position = Vector2.Lerp(pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));

        if(gameController.playerTime >= 5 && gameController.playerTime < 15)
        {
            spawnMin = 3;
            spawnMax = 7;
        }
        else if(gameController.playerTime >= 15 && gameController.playerTime < 20)
        {
            spawnMin = 2;
            spawnMax = 5;
        }
        else if(gameController.playerTime >= 20)
        {
            spawnMin = 1.5f;
            spawnMax = 4;
        }

        randomChance = Random.Range(0, 100);

        if(randomChance <= 33 && spawnTime <= 1)
        {
           enemy = Instantiate(enemies[0], transform.position, Quaternion.identity);
           rb = enemy.GetComponent<Rigidbody2D>();
           rb.AddForce(spawnPoint.up * asteroidSpeed, ForceMode2D.Impulse);
           spawnTime = Random.Range(spawnMin,spawnMax);
        }
        else if((randomChance <= 66 && randomChance > 33) && spawnTime <= 1)
        {
           enemy = Instantiate(enemies[1], transform.position, Quaternion.identity);
           rb = enemy.GetComponent<Rigidbody2D>();
           rb.AddForce(spawnPoint.up * (asteroidSpeed/2), ForceMode2D.Impulse);
           spawnTime = Random.Range(spawnMin,spawnMax);
        }
        else if((randomChance <= 100 && randomChance > 66) && spawnTime <= 1)
        {
           enemy = Instantiate(enemies[2], transform.position, Quaternion.identity);
           rb = enemy.GetComponent<Rigidbody2D>();
           rb.AddForce(spawnPoint.up * (asteroidSpeed*2), ForceMode2D.Impulse);
           spawnTime = Random.Range(spawnMin,spawnMax);
        }

        spawnTime -= Time.deltaTime;
        Debug.Log(spawnTime);
    }

}
