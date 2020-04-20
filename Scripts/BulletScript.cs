﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private GameObject bullet;
    public float shootTimer;

    public CameraShake cameraShake;

    //Recoil Variables
    public float maxOffsetDistance;
    public float recoilAccel;
    public float recoilInitSpeed;
    private bool recoilInEffect;
    private bool goingToStartPos;
    private Vector3 offsetPosition;
    private Vector3 recoilSpeed;
    public AudioSource fire;

    void Start() 
    {
        recoilSpeed = Vector3.zero;
        offsetPosition = Vector3.zero;

        recoilInEffect = false;
        goingToStartPos = false;    

        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRecoil();

        if(Input.GetButtonDown("Fire1") && shootTimer <= 0)
        {
            shootTimer = 0.15f;
            Shoot();
            AddRecoil();
            StartCoroutine(cameraShake.Shake(.1f, .1f));
        }
        shootTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        fire.Play();
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    void AddRecoil()
    {
        recoilInEffect = true;
        goingToStartPos = false;
        recoilSpeed = transform.up * recoilInitSpeed;
    }

    void UpdateRecoil()
    {
        if(recoilInEffect == false)
        {
            return;
        }

        //Speed & Position
        recoilSpeed += (-offsetPosition.normalized) * recoilAccel * Time.deltaTime;
        Vector3 newOffsetPosition = offsetPosition + recoilSpeed * Time.deltaTime;
        Vector3 newTranformPos = transform.position - offsetPosition;

        if(newOffsetPosition.magnitude > maxOffsetDistance)
        {
            recoilSpeed = Vector3.zero;
            goingToStartPos = true;
            newOffsetPosition = offsetPosition.normalized * maxOffsetDistance;
        }
        else if(goingToStartPos == true && newOffsetPosition.magnitude > offsetPosition.magnitude)
        {
            transform.position -= offsetPosition;
            offsetPosition = Vector3.zero;

            //Update booleans
            recoilInEffect = false;
            goingToStartPos = false;
            return;
        }

        transform.position = newTranformPos + newOffsetPosition;
        offsetPosition = newOffsetPosition;
    }

}
