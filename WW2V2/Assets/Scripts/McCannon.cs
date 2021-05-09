using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McCannon : ShipSystems
{
    public GameObject messerschmittShip;
    private int shipCount = 0;
    public float spawnDelay = 20f;
    Transform spawnpoint1;
    Transform spawnpoint2;
    private int maxShips = 6;
    public AudioSource siren;
    public AudioSource charge;
    void Awake()
    {
        
        ammo = 1000000;
        maxAmmo = 1000000;
        spawnpoint1 = transform.Find("ShipSpawn").gameObject.transform.GetChild(0).gameObject.transform;
        spawnpoint2 = transform.Find("ShipSpawn").gameObject.transform.GetChild(1).gameObject.transform;
        StartCoroutine(pathDetectionCouroutine());
        StartCoroutine(leaderDetectionCouroutine());
        StartCoroutine(enemyDetectionCouroutine());
        StartCoroutine(deployDefence());
        StartCoroutine(fireCannons());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 10)
        {
            explodeShip();
        }
    }
    public new GameObject getLeader()
    {
        GameObject leader = null;
        squadLeader = true;
        leader = transform.gameObject;
        return leader;
    }
    IEnumerator deployDefence() 
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if(targetEnemy != null)
            {
                float dist = Vector3.Distance(targetEnemy.transform.position, transform.position);
                if(dist < detectionRange && shipCount < maxShips)
                {
                    if(!siren.isPlaying)
                    {
                        siren.Play();
                    }
                    Instantiate(messerschmittShip, spawnpoint1.position, transform.rotation);
                    Instantiate(messerschmittShip, spawnpoint2.position, transform.rotation);
                    spawnDelay += spawnDelay;
                }
            }
            shipCount = GameObject.FindGameObjectsWithTag("jibinis").Length;
        }
    }

    public new IEnumerator leaderDetectionCouroutine()
    {
        while(true)
        {
            if(targetLeader == null)
            {
                targetLeader = getLeader();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator fireCannons()
    {
        while(true) 
        {
            charge.Play();
            yield return new WaitForSeconds(4f);
            shootFX.Play();
            yield return new WaitForSeconds(0.5f);
            shoot();
            yield return new WaitForSeconds(6f);
        }
    }

    void explodeShip()
    {
        GameObject explosionObj = transform.Find("ExplosionMC").gameObject;
        int explosionCount = explosionObj.transform.childCount;
        for(int i = 0; i < explosionCount; i++)
        {
            if(!explosion.isPlaying)
            {
                explosion.Play();
            }
            explosionObj.transform.GetChild(i).gameObject.transform.Find("BigExplosion").GetComponent<ParticleSystem>().Play();
        }
        float totalTime = 4f;
        Destroy(gameObject, totalTime);
    }

    void shoot()
    {
        Instantiate(bulletPrefab, transform.Find("Cannons").gameObject.transform.GetChild(0).gameObject.transform.position, transform.rotation);
        Instantiate(bulletPrefab, transform.Find("Cannons").gameObject.transform.GetChild(1).gameObject.transform.position, transform.rotation);
        Instantiate(bulletPrefab, transform.Find("Cannons").gameObject.transform.GetChild(2).gameObject.transform.position, transform.rotation);
        Instantiate(bulletPrefab, transform.Find("Cannons").gameObject.transform.GetChild(3).gameObject.transform.position, transform.rotation);
    }
}
