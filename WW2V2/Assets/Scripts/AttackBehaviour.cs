using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public Vector3 enemyPos;
    private Vector3 offset;
    private Vector3 offsetTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

        // offset = transform.position - enemyTarget.transform.position;

        // offset = Quaternion.Inverse(enemyTarget.transform.rotation) * offset;
        StartCoroutine(shootingCouroutine());
        StartCoroutine(reload());
    }

    public void OnDrawGizmos()
    {
        if (Application.isPlaying && isActiveAndEnabled)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, enemyPos);
        }
    }

    public override Vector3 Calculate()
    {
        // offsetTarget = enemyTarget.transform.TransformPoint(offset);
        float dist = Vector3.Distance(enemyTarget.transform.position, transform.position);
        float time = dist / ship.maxSpeed;
        enemyPos = offsetTarget + (enemyTarget.GetComponent<Ship>().velocity * time);
        return ship.SeekForce(enemyPos);
    }

    public void shoot()
    {
        Vector3 distanceToEnemy = shipInfo.targetEnemy.transform.position - shipInfo.transform.position;
        if(Vector3.Angle(shipInfo.transform.forward, distanceToEnemy) < 45 && distanceToEnemy.magnitude <= shipInfo.shootingRange)
        {
            if(shipInfo.ammo > 0)
            {
                GameObject bullet = GameObject.Instantiate(shipInfo.bulletPrefab, shipInfo.transform.position, shipInfo.transform.rotation);
                shipInfo.ammo --;
            }
        }
    }

    IEnumerator shootingCouroutine()
    {
        shoot();
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(5f);
        if(shipInfo.ammo < 10)
        {
            shipInfo.ammo++;
        }
    }
}
