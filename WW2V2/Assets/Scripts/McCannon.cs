using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McCannon : ShipSystems
{
    public GameObject messerschmittShip;
    public int maxShips = 0;
    Transform spawnpoint1;
    Transform spawnpoint2;
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
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
            yield return new WaitForSeconds(5f);
            if(targetEnemy != null)
            {
                float dist = Vector3.Distance(targetEnemy.transform.position, transform.position);
                if(dist < detectionRange && maxShips < 4)
                {
                    Instantiate(messerschmittShip, spawnpoint1.position, transform.rotation);
                    Instantiate(messerschmittShip, spawnpoint2.position, transform.rotation);
                }
            }
            maxShips = GameObject.FindGameObjectsWithTag("jibinis").Length;
        }
    }
}
