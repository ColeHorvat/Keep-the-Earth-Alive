using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public int totalHealth;
    public int currentHealth;

    public AudioSource destroySource;
    public AudioSource hitEarthSource;
    public AudioSource hitAsteroidSource;
    private bool soundPlayed;

    public GameObject asteroidParticles;

    void Start()
    {
        currentHealth = totalHealth;
        soundPlayed = false;
    }

    void Update() 
    {
        if(currentHealth <= 0)
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            destroySource.Play();
            Instantiate(asteroidParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject, destroySource.clip.length);
        }    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Earth") || other.CompareTag("Gun"))
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            hitEarthSource.Play();
            Destroy(this.gameObject, hitEarthSource.clip.length);
        }    

        if(other.CompareTag("Bullet"))
        {
            hitAsteroidSource.Play();
            currentHealth--;
            Destroy(other.gameObject);
        }
    }
}
