using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Ship))]
public class ShipSystems : MonoBehaviour
{

    public float health;
    public float ammo;
    public GameObject bulletPrefab;
    public string enemyTag;
    public GameObject targetEnemy;
    public float shootingRange;
    public float detectionRange;
    public GameObject cameras;
    public GameObject primaryTurrets;

    void Awake()
    {
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

    IEnumerator enemyDetectionCouroutine()
    {
        while(true)
        {
            if(targetEnemy == null)
            {
                targetEnemy = getClosestEnemy();
            }
            yield return new WaitForSeconds(10f);
        }
    }
}
