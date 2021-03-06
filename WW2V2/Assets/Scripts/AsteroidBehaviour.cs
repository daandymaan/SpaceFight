﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public float rotatespeed = 10f;
    public GameObject fracturedRock;
    public int rotationDirection = 0;
    Vector3 direction;
    float minRotate = 10f;
    float maxRotate = 50f;
    void Start()
    {
        rotatespeed = Mathf.Clamp(rotatespeed, minRotate, maxRotate);
        switch(rotationDirection)
        {
            case 0:
                direction = Vector3.up;
                break;
            case 1:
                direction = Vector3.down;
                break;
            case 2:
                direction = Vector3.left;
                break;
            case 3:
                direction = Vector3.right;
                break;
            case 4:
                direction = Vector3.back;
                break;
            case 5:
                direction = Vector3.forward;
                break;
            default:
                direction = Vector3.forward;
                break;
        }
    }

    void Update()
    {
        transform.localRotation *= Quaternion.AngleAxis(rotatespeed * Time.deltaTime, direction);
    }

    void fracture()
    {
        GameObject fracAsteroid = Instantiate(fracturedRock, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(fracAsteroid, 5f);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "ostur" ||
         collisionInfo.gameObject.tag == "jibinis" ||
         collisionInfo.gameObject.tag == "j_laser" ||
         collisionInfo.gameObject.tag == "o_laser")
        {
            fracture();
        }
    }
}
