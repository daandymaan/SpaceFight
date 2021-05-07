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
        StartCoroutine(shootingCouroutine());
        StartCoroutine(reload());
    }

    void OnDisable()
    {
        enemyTarget = null;
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
        enemyPos = enemyTarget.transform.position ;
        return ship.SeekForce(enemyPos);
    }

    public void shoot()
    {
        shipInfo.shootFX.Play();
        Vector3 distanceToEnemy = shipInfo.targetEnemy.transform.position - shipInfo.transform.position;
        if(Vector3.Angle(shipInfo.transform.forward, distanceToEnemy) < 45 && distanceToEnemy.magnitude <= shipInfo.shootingRange)
        {
            if(shipInfo.ammo > 0)
            {
                shipInfo.GetComponent<AudioSource>().Play();
                GameObject bullet1 = GameObject.Instantiate(shipInfo.bulletPrefab, shipInfo.primaryTurrets.transform.GetChild(0).gameObject.transform.position, shipInfo.transform.rotation);
                GameObject bullet2 = GameObject.Instantiate(shipInfo.bulletPrefab, shipInfo.primaryTurrets.transform.GetChild(1).gameObject.transform.position, shipInfo.transform.rotation);
                shipInfo.ammo --;
            }
        }
    }

    IEnumerator shootingCouroutine()
    {
        shoot();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator reload()
    {

        if(shipInfo.ammo <= 5)
        {
            yield return new WaitForSeconds(10f);
            shipInfo.ammo = shipInfo.maxAmmo;
        }
        yield return new WaitForSeconds(0.1f);
    }
}
