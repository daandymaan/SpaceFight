using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        float dist = Vector3.Distance(enemyTarget.transform.position, transform.position);
        float time = dist / ship.maxSpeed;

        enemyPos = enemyTarget.transform.position + (enemyTarget.GetComponent<Ship>().velocity * time);
        shoot();
        return ship.SeekForce(enemyPos);
    }

    public void shoot()
    {
        Vector3 distanceToEnemy = ship.targetEnemy.transform.position - ship.transform.position;
        if(Vector3.Angle(ship.transform.forward, distanceToEnemy) < 45 && distanceToEnemy.magnitude <= ship.shootingRange)
        {
            if(ship.ammo > 0)
            {
                GameObject bullet = GameObject.Instantiate(ship.bulletPrefab, ship.transform.position + ship.transform.forward * 2, ship.transform.rotation);
                ship.ammo --;
            }
            else
            {
                StartCoroutine(reload());
            }
            
        }
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(5f);
        ship.ammo = 10;
    }
}
