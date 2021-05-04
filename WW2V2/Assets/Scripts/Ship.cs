﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float mass = 1;
    [Range(0.0f, 10.0f)]
    public float damping = 0.01f;
    [Range(0.0f, 1.0f)]
    public float banking = 0.1f;
    public float maxSpeed = 50.0f;
    public float maxForce = 10.0f;
    // public float health = 10;
    // public float ammo = 10;
    // public GameObject bulletPrefab;
    // public string enemyTag;
    // public GameObject targetEnemy;
    // public float shootingRange;
    // public float detectionRange;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + acceleration);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + force * 10);
    }

    // Use this for initialization
    void Start()
    {

        SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

        foreach (SteeringBehaviour b in behaviours)
        {
            this.behaviours.Add(b);            
        }
    }

    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }

    public Vector3 ArriveForce(Vector3 target, float slowingDistance = 40.0f)
    {
        Vector3 toTarget = target - transform.position;

        float distance = toTarget.magnitude;

        
        if (distance > 0)
        {        
            float ramped = maxSpeed * (distance / slowingDistance);

            float clamped = Mathf.Min(ramped, maxSpeed);
            Vector3 desired = clamped * (toTarget / distance);

            return desired - velocity;
        }
        else
        {
            return Vector3.zero;
        }        
    }
    

    Vector3 Calculate()
    {
        force = Vector3.zero;
        foreach (SteeringBehaviour b in behaviours)
        {
            if (b.isActiveAndEnabled)
            {
                force += b.Calculate() * b.weight;      
                float f = force.magnitude;
                if (f >= maxForce)
                {
                    force = Vector3.ClampMagnitude(force, maxForce);
                    break;
                }                         
            }
        }
        return force;
    }

    // Update is called once per frame
    void Update()
    {
        force = Calculate();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        
        if (velocity.magnitude > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);

            transform.position += velocity * Time.deltaTime;
            velocity *= (1.0f - (damping * Time.deltaTime));
        }
    }
}
