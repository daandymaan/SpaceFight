using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public Vector3 enemyPos;
    private Vector3 offset;
    private Vector3 offsetTarget;
    public void OnDrawGizmos()
    {
        if (Application.isPlaying && isActiveAndEnabled)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, enemyPos);
        }
    }
    void OnEnable()
    {
        // offset = transform.position - enemyTarget.transform.position;

        // offset = Quaternion.Inverse(enemyTarget.transform.rotation) * offset;
    }

    public override Vector3 Calculate()
    {
        // offsetTarget = enemyTarget.transform.TransformPoint(offset);
        float dist = Vector3.Distance(enemyTarget.transform.position, transform.position);
        float time = dist / ship.maxSpeed;

        enemyPos = offsetTarget + (enemyTarget.GetComponent<Ship>().velocity * time);

        return ship.SeekForce(enemyPos);
    }

    void Start() {
        
    }
}
