using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Ship))]
public class ShipSystems : MonoBehaviour
{

    public float health;
    public float ammo;
    public float maxAmmo;
    public GameObject bulletPrefab;
    public string enemyTag;
    public GameObject targetEnemy;
    public GameObject targetLeader;
    public bool squadLeader;
    public float shootingRange;
    public float detectionRange;
    public GameObject cameras;
    public GameObject primaryTurrets;
    public Path path;
    public AudioSource deathSwoopFX;
    public AudioSource shootFX;
    public AudioSource explosion;

    void Awake()
    {
        StartCoroutine(pathDetectionCouroutine());
        StartCoroutine(leaderDetectionCouroutine());
        StartCoroutine(enemyDetectionCouroutine());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "j_laser" && transform.tag == "ostur")
        {
            health--;
        } 
        if(collisionInfo.gameObject.tag == "o_laser" && transform.tag == "jibinis")
        {
            health--;
        } 
        
    }

    public GameObject getClosestEnemy()
    {
        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Vector3 position = transform.position;
        foreach(GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        return closestEnemy;
    }

    public GameObject getLeader()
    {
        GameObject leader = null;
        GameObject[] squad = GameObject.FindGameObjectsWithTag(transform.tag);
        leader = squad[squad.Length-1];
        if(leader == transform.gameObject)
        {
            squadLeader = true;
        }
        return leader;
    }

    public Path getPath() 
    {
        if(transform.tag == "ostur")
        {
            GameObject osturRoute = GameObject.Find("OsturPath").gameObject;
            return osturRoute.GetComponent<Path>();
        }
        else if(transform.tag == "jibinis")
        {
            GameObject jibinisRoute = GameObject.Find("JibinisPath").gameObject;
            return jibinisRoute.GetComponent<Path>();
        }
        else 
        {
            return null;
        }
    }

    public IEnumerator enemyDetectionCouroutine()
    {
        while(true)
        {
            if(targetEnemy == null)
            {
                targetEnemy = getClosestEnemy();
            } 
            else 
            {
                float distanceFromCurrentEnemy = Vector3.Distance(transform.position, targetEnemy.transform.position);
                float distanceFromNearestEnemy = Vector3.Distance(transform.position, getClosestEnemy().transform.position);
                if(distanceFromCurrentEnemy > detectionRange)
                {
                    targetEnemy = getClosestEnemy();
                }
                if(distanceFromNearestEnemy < shootingRange && distanceFromCurrentEnemy > shootingRange)
                {
                    targetEnemy = getClosestEnemy();
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
    public IEnumerator leaderDetectionCouroutine()
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
    public IEnumerator pathDetectionCouroutine()
    {
        while(true)
        {
            if(path == null)
            {
                path = getPath();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
