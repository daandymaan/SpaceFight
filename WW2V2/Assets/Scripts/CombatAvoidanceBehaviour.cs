using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAvoidanceBehaviour : SteeringBehaviour
{
    public GameObject enemyTarget;
    public Vector3 target = Vector3.zero;

    public List<Vector3> loopPoints = new List<Vector3>();
    public int nextPoint;
    private bool loopCompleted;
    public float radius = 10;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable() {
        loopPoints.Clear();
        nextPoint = 0;
        loopCompleted = false;
        getLoopPoints();
    }
    private void OnDisable() {
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public override Vector3 Calculate()
    {
        if(loopCompleted == false)
        {
            if(Vector3.Distance(ship.transform.position, loopPoints[nextPoint]) < 5){
                return ship.SeekForce(getNextPoint());
            }
            else
            {
                return ship.SeekForce(loopPoints[nextPoint]);
            }

        } 
        else 
        {
            return (ship.transform.position + (ship.transform.forward * radius));
        }

    }

    public void getLoopPoints()
    {
        float theta = Mathf.PI * 2.0f / (float) radius;
        for(int i = 0; i < radius/2; i++)
        {
            Vector3 pos = new Vector3(Mathf.Sin(theta * i) * (ship.transform.position.x + (ship.transform.up.x * radius)), ship.transform.position.y, Mathf.Cos(theta * i) * (ship.transform.position.z + (ship.transform.up.z * radius)));
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = transform.TransformPoint(pos);
            loopPoints.Add(pos);
        }
    }

    public void getZigZagPoints()
    {
        
    }

    public Vector3 getNextPoint()
    {
        if(nextPoint < loopPoints.Count -1 )
        {
            Vector3 point = loopPoints[nextPoint];
            nextPoint++;
            return point;
        } 
        else 
        {
            loopCompleted = true;
            return loopPoints[nextPoint];
        }
    }
}
