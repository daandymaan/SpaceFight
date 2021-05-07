using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeaderBehaviour : SteeringBehaviour
{
    public GameObject leader;
    public Vector3 leaderPos;
    public Vector3 worldTarget;
    public Vector3 targetPosition;
    Vector3 offset;
    float distanceFromLeader;
    void OnEnable()
    {
        
    }
    void OnDisable()
    {
        leader = null;
    }

    void Start()
    {
        
    }

    void Update()
    {
        distanceFromLeader = Vector3.Distance(ship.transform.position, leader.transform.position);
        if(distanceFromLeader < 10)
        {
            StartCoroutine(getOffsetValue());
        }
    }

    public override Vector3 Calculate()
    {
        if(distanceFromLeader < 10)
        {
            worldTarget = leader.transform.TransformPoint(offset);
            float dist = Vector3.Distance(transform.position, worldTarget);
            float time = dist / ship.maxSpeed;

            targetPosition = worldTarget + (leader.GetComponent<Ship>().velocity * time);
            return ship.ArriveForce(targetPosition);
        }
        else 
        {
            float dist = Vector3.Distance(leader.transform.position, transform.position);
            float time = dist / ship.maxSpeed;
            leaderPos = leader.transform.position;
            return ship.SeekForce(leaderPos);
        }
    }

    IEnumerator getOffsetValue()
    {
        offset = transform.position - leader.transform.position;
        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
        yield return new WaitForEndOfFrame();
    }
}
