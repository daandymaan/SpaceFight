using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueBehaviour : SteeringBehaviour
{
    public Ship enemyTarget;
    public Vector3 enemyPos;

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

        enemyPos = enemyTarget.transform.position + (enemyTarget.velocity * time);

        return ship.SeekForce(enemyPos);
    }
}
