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
    public List<Vector3> cameraPos = new List<Vector3>();
    public GameObject primaryTurrets;
    public List<Vector3> pTurretsPos = new List<Vector3>();

    void Awake()
    {
        getCameraPos();
        getPrimaryTurretsPos();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetEnemy == null)
        {
            targetEnemy = getClosestEnemy();
        }
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

    public void getCameraPos()
    {
        cameraPos.Clear();
        for(int i = 0; i < cameras.transform.childCount; i++)
        {
            cameraPos.Add(cameras.transform.GetChild(i).gameObject.transform.position);
        }
    }

    public void getPrimaryTurretsPos()
    {
        pTurretsPos.Clear();
        for(int i = 0; i < primaryTurrets.transform.childCount; i++)
        {
            pTurretsPos.Add(primaryTurrets.transform.GetChild(i).gameObject.transform.position);
        }
    }
}
