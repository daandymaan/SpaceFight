using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : SteeringBehaviour
{
    public float frequency = 0.1f;
    public float amplitude = 80;
    public float radius = 10f;
    public float distance = 1;
    public float theta = 0;
    public float rotateSpeed = 300f;
    public enum Axis { Horizontal, Vertical};
    public Axis axis = Axis.Horizontal;
    public Vector3 waveTarget;
    public Vector3 globalTarget;

    void OnEnable()
    {
        ship.maxForce += 10;
        ship.maxSpeed += 10;
        setTrailEffects();
        StartCoroutine(deathDive());
        StartCoroutine(explosion());

    }
    void Start()
    {
        
    }

    void Update()
    {
        ship.transform.localRotation *= Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, Vector3.back);
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

    IEnumerator deathDive()
    {
        while(true) 
        {
            yield return new WaitForSeconds(0.3f);
            if(rotateSpeed < 1000)
            {
                rotateSpeed += 25f;
            }
        }
    }

    public void setTrailEffects()
    {
        GameObject trails = ship.transform.Find("Trails").gameObject;
        Destroy(trails.transform.GetChild(2).gameObject);
        Destroy(trails.transform.GetChild(0).gameObject);
        GameObject smokeGen = ship.transform.Find("SmokeGen").gameObject;
        smokeGen.transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator explosion()
    {
        yield return new WaitForSeconds(5f);
        explodeShip();
    }
    public void explodeShip()
    {
        GameObject explosion = ship.transform.Find("Explosion").gameObject.transform.Find("BigExplosion").gameObject;
        ParticleSystem explosionParticleSystem =  explosion.GetComponent<ParticleSystem>();
        float totalTime = explosionParticleSystem.main.duration - 1.5f;
        Destroy(ship.transform.gameObject, totalTime);
        explosionParticleSystem.Play();
    }
}
