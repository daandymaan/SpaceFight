using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public Vector3 target = Vector3.zero;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (enemyTarget != null)
            {
                target = enemyTarget.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }

    public override Vector3 Calculate()
    {
        return - ship.SeekForce(target);
    }

    public void Update()
    {
        if(enemyTarget != null)
        {
            target = enemyTarget.transform.position;
        }
    }
}
