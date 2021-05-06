using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAvoidanceBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public float frequency = 0.3f;
    public float amplitude = 80;
    public float radius = 10f;
    public float distance = 1;
    public float theta = 0;
    public enum Axis { Horizontal, Vertical};
    public Axis axis = Axis.Horizontal;
    public Vector3 waveTarget;
    public Vector3 globalTarget;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable() 
    {
        ship.maxForce+= 10;
        ship.maxSpeed+= 10;
        StartCoroutine(changeAxis());
    }

    void OnDisable()
    {
        ship.maxForce -= 10;
        ship.maxSpeed -= 10;
        enemyTarget = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Vector3 Calculate()
    {
        float n = Mathf.Sin(theta);
        float angle = n * amplitude * Mathf.Deg2Rad;
        Vector3 rot = transform.rotation.eulerAngles;    
        rot.x = 0;
        if (axis == Axis.Horizontal)
        {
            waveTarget.x = Mathf.Sin(angle);
            waveTarget.z = Mathf.Cos(angle);
            rot.z = 0;
        }
        else
        {
            waveTarget.y = Mathf.Sin(angle);
            waveTarget.z = Mathf.Cos(angle);
        }
        Vector3 localtarget = waveTarget + Vector3.forward * distance;
        globalTarget = transform.position + Quaternion.Euler(rot) * localtarget;

        theta += frequency * Time.deltaTime * Mathf.PI * 2.0f;

        return ship.SeekForce(globalTarget);
    }

    IEnumerator changeAxis()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
            if(axis == Axis.Horizontal)
            {
                axis = Axis.Vertical;
            }
            else 
            {
                axis = Axis.Horizontal;
            }
        }
    }
} 
